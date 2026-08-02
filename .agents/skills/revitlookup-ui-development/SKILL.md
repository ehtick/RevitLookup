---
name: revitlookup-ui-development
description: >
  Add a UI surface to RevitLookup as a WPF-UI page or window backed by a view-model contract both hosts implement, letting the same view run in the Revit add-in and the standalone Playground.
  USE FOR: defining a view-model interface, building the shared view, implementing the view model twice (the Revit-backed production version and the Playground mock), wiring navigation, and customizing per-type display with a template selector.
  DO NOT USE FOR: teaching the engine about a Revit type (use revitlookup-descriptor-model), or changing the shared WPF-UI control library in the LookupEngine.UI submodule.
license: MIT
---

# RevitLookup UI Development

RevitLookup runs one shared WPF codebase in two hosts: the production add-in inside `Revit.exe` and a standalone Playground that mocks the Revit layer.
A view binds to a view-model interface from `RevitLookup.Abstractions`; each host supplies its own implementation, and every Revit contract has a Playground mock.
The UI stack is WPF-UI (`Wpf.Ui.*`, from the `LookupEngine.UI` submodule) with `CommunityToolkit.Mvvm`.

## When to use

- Adding a page, window, or dialog, or the view model behind it.

## When not to use

- Adding decomposition support for a Revit type, which is a descriptor (see revitlookup-descriptor-model).

## Workflow

### Step 1: Define the view-model contract in Abstractions

Declare the view model as an interface named `IXViewModel` under `source/RevitLookup.Abstractions/ViewModels/<Area>/`, exposing the observable state and commands the view binds.
Keep the contract without Revit dependencies.

### Step 2: Build the shared view in the UI framework

Add the view under `source/RevitLookup.UI.Framework/Views/<Area>/` as a `sealed partial` class implementing `Wpf.Ui.Abstractions.Controls.INavigableView<IXViewModel>` (a `Page`), or deriving from `FluentWindow` / `ContentDialog`.
Inject the view-model interface and any framework services, set `ViewModel`, set `DataContext = this`, and call `InitializeComponent()`.

```csharp
public sealed partial class WidgetPage : INavigableView<IWidgetViewModel>
{
    public WidgetPage(IWidgetViewModel viewModel, IThemeWatcherService themeWatcherService)
    {
        themeWatcherService.Watch(this);
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public IWidgetViewModel ViewModel { get; }
}
```

### Step 3: Implement the view model in both hosts

Write the production, Revit-backed implementation in the `RevitLookup` add-in project, and the mock in `source/RevitLookup.UI.Playground/Mocks/ViewModels/<Area>/` using Bogus sample data.
Both implement `IXViewModel` and use `CommunityToolkit.Mvvm` (`ObservableObject`, `[ObservableProperty]`, `[RelayCommand]`).
The class name must end in `ViewModel`; the scan matches that suffix.

### Step 4: Register nothing — Scrutor scans by convention

`AddViews()` (`source/RevitLookup/Views/ViewsRegistration.cs`) scans the assembly and registers every `FluentWindow`, `ContentDialog`, and `Page` (a navigable `Page` scoped, others transient); `AddViewModels()` (`source/RevitLookup/ViewModels/ViewModelsRegistration.cs`) registers every `*ViewModel` `AsImplementedInterfaces`.
The Playground has its own pair under `source/RevitLookup.UI.Playground/Views/` and `.../ViewModels/`, which also scans the mocks.
A new view and its view models need no manual DI registration; follow the naming and base-type conventions and they are picked up.

### Step 5: Navigate and customize display

Move between pages through the injected `INavigationService`.
To render a decomposed value differently by type, add a named `DataTemplate` and extend the template selector, not a branch in code-behind; value formatting lives in `source/RevitLookup.UI.Framework/Converters/`.
The selectors and their resource dictionaries live twice, once per host, because the add-in copy resolves Revit types and the Playground copy must stay BCL-only: `source/RevitLookup/Views/Styles/{ObjectsTree,MembersGrid}/` and `source/RevitLookup.UI.Playground/Mocks/Styles/{ObjectsTree,MembersGrid}/`.
Change both in the same commit, and keep the two trees at mirrored paths.

### Step 6: Verify

Iterate on the UI in the Playground under the plain `Debug` configuration, then confirm it in the add-in under a `Debug.R##` configuration inside Revit.

```shell
dotnet run --project source/RevitLookup.UI.Playground -c Debug
```

## Validation

- [ ] The view-model contract is a Revit-free `IXViewModel` interface in `RevitLookup.Abstractions`.
- [ ] The shared view lives in `RevitLookup.UI.Framework/Views/`, implements `INavigableView<IXViewModel>` (or derives from `FluentWindow`/`ContentDialog`), and sets `ViewModel` + `DataContext = this`.
- [ ] The view model is implemented twice — the Revit-backed version in the add-in and a Playground mock — both ending in `ViewModel`.
- [ ] No manual DI registration was added; the Scrutor scans pick the view and view models up.
- [ ] Per-type display is handled through a `DataTemplate` and the template selector, not code-behind branching.
- [ ] The surface runs in both the Playground (`Debug`) and the add-in (`Debug.RNN`).

## Common Pitfalls

| Pitfall                                                                  | Correct approach                                                                         |
|--------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| Referencing a Revit type from the view-model interface                   | Keep the contract in `Abstractions` Revit-free for the Playground to mock it.            |
| Implementing the view model only for the add-in                          | Provide a Playground mock too; every contract has one.                                   |
| Manually registering the view or view model in the host composition root | Follow the naming/base-type conventions; Scrutor's `AddViews`/`AddViewModels` scan them. |
| A view-model class name not ending in `ViewModel`                        | End the name in `ViewModel` for the scan to register it by its interface.                |
| Branching on value type in code-behind to format display                 | Add a `DataTemplate` and extend `TreeViewItemTemplateSelector`.                          |
