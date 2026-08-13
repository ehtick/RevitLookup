// Copyright (c) Lookup Foundation and Contributors
// 
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
// 
// THIS PROGRAM IS PROVIDED "AS IS" AND WITH ALL FAULTS.
// NO IMPLIED WARRANTY OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE IS PROVIDED.
// THERE IS NO GUARANTEE THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.

using System.Reflection;
using Autodesk.Revit.UI;
using Microsoft.Extensions.Logging;
using Nice3point.Revit.Toolkit.External;

namespace RevitLookup.Decomposition.EventsMonitor;

/// <summary>
///     Monitors every event exposed by the RevitAPI and RevitAPIUI assemblies and republishes them through <see cref="EventInvoked" />.
/// </summary>
/// <param name="logger">The logger used to report events without a supported target.</param>
public sealed partial class EventsMonitoringService(ILogger<EventsMonitoringService> logger)
{
    private static readonly MethodInfo HandlerMethod = typeof(EventHandlerWrapper).GetMethod(nameof(EventHandlerWrapper.OnEvent))!;

    private readonly Assembly[] _assemblies = AppDomain.CurrentDomain
        .GetAssemblies()
        .Where(static assembly =>
        {
            var name = assembly.GetName().Name;
            return name is "RevitAPI" or "RevitAPIUI";
        })
        .Take(2)
        .ToArray();

    private readonly HashSet<string> _denyList =
    [
        nameof(UIApplication.Idling),
        nameof(Autodesk.Revit.ApplicationServices.Application.ProgressChanged)
    ];

    private readonly Dictionary<EventInfo, Delegate> _handlersMap = new(16);
    private Action<object, string>? _eventInvoked;

    /// <summary>
    ///     An event that is raised when a monitored Revit API event fires, carrying the original event args and the name of the event.
    /// </summary>
    /// <remarks>
    ///     Subscribing a first handler discovers and subscribes to every non-denied event on <see cref="Autodesk.Revit.ApplicationServices.Application" />, <see cref="Document" />, and <see cref="UIApplication" />.
    ///     Removing the last handler unsubscribes from all of them.
    /// </remarks>
    public event Action<object, string> EventInvoked
    {
        add
        {
            _eventInvoked += value;
            SubscribeEvent.Raise();
        }
        remove
        {
            _eventInvoked -= value;
            if (_eventInvoked is null)
            {
                UnsubscribeEvent.Raise();
            }
        }
    }

    [ExternalEvent(AllowDirectInvocation = true)]
    private void Subscribe()
    {
        if (_handlersMap.Count > 0)
        {
            return;
        }

        foreach (var dll in _assemblies)
        foreach (var type in dll.GetTypes().Where(static type => type is { IsEnum: false, IsValueType: false, IsInterface: false }))
        foreach (var eventInfo in type.GetEvents())
        {
            if (_denyList.Contains(eventInfo.Name))
            {
                continue;
            }

            var targets = FindValidTargets(eventInfo.ReflectedType);
            if (targets.Length == 0)
            {
                LogMissingTarget(logger, eventInfo.ReflectedType, eventInfo.Name);
                break;
            }

            var wrapper = new EventHandlerWrapper(eventInfo.Name, this);
            var eventHandler = Delegate.CreateDelegate(eventInfo.EventHandlerType!, wrapper, HandlerMethod);

            foreach (var target in targets)
            {
                eventInfo.AddEventHandler(target, eventHandler);
            }

            _handlersMap.Add(eventInfo, eventHandler);
            LogObserving(logger, eventInfo.ReflectedType, eventInfo.Name);
        }
    }

    [ExternalEvent(AllowDirectInvocation = true)]
    private void Unsubscribe()
    {
        foreach (var (eventInfo, handler) in _handlersMap)
        {
            var targets = FindValidTargets(eventInfo.ReflectedType);
            foreach (var target in targets)
            {
                eventInfo.RemoveEventHandler(target, handler);
            }
        }

        _handlersMap.Clear();
    }

    private static object[] FindValidTargets(Type? targetType)
    {
        return targetType switch
        {
            _ when targetType == typeof(Document) => RevitApiContext.Application.Documents.Cast<object>().ToArray(),
            _ when targetType == typeof(Autodesk.Revit.ApplicationServices.Application) => [RevitApiContext.Application],
            _ when targetType == typeof(UIApplication) => [RevitContext.UiApplication],
            _ => []
        };
    }

    [LoggerMessage(LogLevel.Debug, "Missing target: {EventType}.{EventName}")]
    private static partial void LogMissingTarget(ILogger<EventsMonitoringService> logger, Type? eventType, string eventName);

    [LoggerMessage(LogLevel.Debug, "Observing: {EventType}.{EventName}")]
    private static partial void LogObserving(ILogger<EventsMonitoringService> logger, Type? eventType, string eventName);

    private sealed class EventHandlerWrapper(string eventName, EventsMonitoringService service)
    {
        [UsedImplicitly(Reason = "Reflection delegate subscription")]
        public void OnEvent(object sender, EventArgs args)
        {
            service._eventInvoked?.Invoke(args, eventName);
        }
    }
}
