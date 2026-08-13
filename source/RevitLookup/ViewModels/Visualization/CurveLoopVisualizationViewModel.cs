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

using Microsoft.Extensions.Logging;
using RevitLookup.Abstractions.Presentation;
using RevitLookup.Abstractions.Settings;
using RevitLookup.Abstractions.ViewModels.Visualization;
using RevitLookup.Visualization;
using Color = System.Windows.Media.Color;

namespace RevitLookup.ViewModels.Visualization;

/// <summary>
///     Represents the view model for curve loop visualization, rendering a <see cref="CurveLoop" /> through a dedicated Revit visualization server.
/// </summary>
/// <param name="settingsService">The service that persists and supplies the curve loop visualization settings.</param>
/// <param name="notificationService">The service used to report rendering failures.</param>
/// <param name="logger">The logger used to record rendering failures.</param>
[UsedImplicitly]
public sealed partial class CurveLoopVisualizationViewModel(
    ISettingsService settingsService,
    INotificationService notificationService,
    ILogger<CurveLoopVisualizationViewModel> logger)
    : ObservableObject, ICurveLoopVisualizationViewModel
{
    private readonly CurveLoopVisualizationServer _server = new();

    /// <inheritdoc />
    [ObservableProperty]
    public partial double Diameter { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.Diameter;

    /// <inheritdoc />
    [ObservableProperty]
    public partial double Transparency { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.Transparency;

    /// <inheritdoc />
    [ObservableProperty]
    public partial Color SurfaceColor { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.SurfaceColor;

    /// <inheritdoc />
    [ObservableProperty]
    public partial Color CurveColor { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.CurveColor;

    /// <inheritdoc />
    [ObservableProperty]
    public partial Color DirectionColor { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.DirectionColor;

    /// <inheritdoc />
    [ObservableProperty]
    public partial bool ShowSurface { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.ShowSurface;

    /// <inheritdoc />
    [ObservableProperty]
    public partial bool ShowCurve { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.ShowCurve;

    /// <inheritdoc />
    [ObservableProperty]
    public partial bool ShowDirection { get; set; } = settingsService.VisualizationSettings.CurveLoopSettings.ShowDirection;

    /// <inheritdoc />
    public double MinThickness => settingsService.VisualizationSettings.CurveLoopSettings.MinThickness;

    /// <inheritdoc />
    /// <exception cref="ArgumentException"><paramref name="curveLoop" /> is not a <see cref="CurveLoop" />.</exception>
    public void RegisterServer(object curveLoop)
    {
        if (curveLoop is not CurveLoop loop)
        {
            throw new ArgumentException("Unexpected CurveLoop type", nameof(curveLoop));
        }

        Initialize();
        _server.RenderFailed += HandleRenderFailure;

        var vertices = CollectVertices(loop);
        _server.Register(vertices);
    }

    /// <inheritdoc />
    public void UnregisterServer()
    {
        _server.RenderFailed -= HandleRenderFailure;
        _server.Unregister();
    }

    private static List<XYZ> CollectVertices(CurveLoop loop)
    {
        var vertices = new List<XYZ>();
        foreach (var curve in loop)
        {
            var curveVertices = curve.Tessellate().ToList();
            foreach (var vertex in curveVertices)
            {
                if (ContainsVertex(vertices, vertex))
                {
                    continue;
                }

                vertices.Add(vertex);
            }
        }

        if (!loop.IsOpen())
        {
            vertices.Add(vertices[0]);
        }

        return vertices;
    }

    private static bool ContainsVertex(List<XYZ> vertices, XYZ vertex)
    {
        foreach (var point in vertices)
        {
            if (point.IsAlmostEqualTo(vertex))
            {
                return true;
            }
        }

        return false;
    }

    private void Initialize()
    {
        UpdateShowSurface(ShowSurface);
        UpdateShowCurve(ShowCurve);
        UpdateShowDirection(ShowDirection);

        UpdateSurfaceColor(SurfaceColor);
        UpdateCurveColor(CurveColor);
        UpdateDirectionColor(DirectionColor);

        UpdateTransparency(Transparency);
        UpdateDiameter(Diameter);
    }

    private void HandleRenderFailure(object? sender, RenderFailedEventArgs args)
    {
        LogRenderError(logger, args.ExceptionObject);
        notificationService.ShowError("Render error", args.ExceptionObject);
    }

    partial void OnSurfaceColorChanged(Color value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.SurfaceColor = value;
        UpdateSurfaceColor(value);
    }

    partial void OnCurveColorChanged(Color value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.CurveColor = value;
        UpdateCurveColor(value);
    }

    partial void OnDirectionColorChanged(Color value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.DirectionColor = value;
        UpdateDirectionColor(value);
    }

    partial void OnDiameterChanged(double value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.Diameter = value;
        UpdateDiameter(value);
    }

    partial void OnTransparencyChanged(double value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.Transparency = value;
        UpdateTransparency(value);
    }

    partial void OnShowSurfaceChanged(bool value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.ShowSurface = value;
        UpdateShowSurface(value);
    }

    partial void OnShowCurveChanged(bool value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.ShowCurve = value;
        UpdateShowCurve(value);
    }

    partial void OnShowDirectionChanged(bool value)
    {
        settingsService.VisualizationSettings.CurveLoopSettings.ShowDirection = value;
        UpdateShowDirection(value);
    }

    private void UpdateSurfaceColor(Color value)
    {
        _server.UpdateSurfaceColor(new Autodesk.Revit.DB.Color(value.R, value.G, value.B));
    }

    private void UpdateCurveColor(Color value)
    {
        _server.UpdateCurveColor(new Autodesk.Revit.DB.Color(value.R, value.G, value.B));
    }

    private void UpdateDirectionColor(Color value)
    {
        _server.UpdateDirectionColor(new Autodesk.Revit.DB.Color(value.R, value.G, value.B));
    }

    private void UpdateDiameter(double value)
    {
        _server.UpdateDiameter(value / 12);
    }

    private void UpdateTransparency(double value)
    {
        _server.UpdateTransparency(value / 100);
    }

    private void UpdateShowSurface(bool value)
    {
        _server.UpdateSurfaceVisibility(value);
    }

    private void UpdateShowCurve(bool value)
    {
        _server.UpdateCurveVisibility(value);
    }

    private void UpdateShowDirection(bool value)
    {
        _server.UpdateDirectionVisibility(value);
    }

    [LoggerMessage(LogLevel.Error, "Render error")]
    private static partial void LogRenderError(ILogger<CurveLoopVisualizationViewModel> logger, Exception exception);
}
