# RevitLookup

RevitLookup is a public open-source Revit add-in that decomposes any element, type, or document at runtime and shows every member in the UI.
It runs in two hosts from one shared codebase: the production add-in inside `Revit.exe`, and a standalone Playground that mocks the Revit layer; UI work needs no running Revit.

## Non-negotiables

* The two hosts stay in sync. Every shared contract the add-in implements against the Revit API has a matching Playground mock; a Revit-backed service, view model, or descriptor ships with its Playground counterpart in the same change.
* The Revit API stays out of the shared layers. `RevitLookup.Abstractions`, `RevitLookup.ServiceDefaults`, and `RevitLookup.UI.Framework` never reference the Revit API; Revit access lives in the `RevitLookup` add-in project alone.
* Touch the Revit API only on the Revit thread. Marshal cross-thread Revit work through the `[ExternalEvent]` source generator; never call the Revit API from a background thread or through `Dispatcher.Invoke`.
* Extend through LookupEngine descriptors, never a special case elsewhere.
* Every type compiles under every supported Revit configuration.
* A change ships with a TUnit test; UI changes are exercised in the Playground.
* Confirm an unfamiliar Revit or .NET API before use through official docs or `gh` (`gh api`, `gh search code`).
* A user-facing change updates `CHANGELOG.md`, the wiki, and the affected XML docs in the same commit.
* A registration extension is named for its net effect on the container: `Add*` when something resolves after the call that did not before, `Configure*` when the call only supplies settings. Split a registration by phase, never by verb.
* The file suffix of an extension class follows the host phase, never the verb of the method inside. `*Registration.cs` holds everything that runs before `Build()`, whether the method reads `Add*` or `Configure*`. A class that carries a second phase is split into a second file.
* `*Configuration.cs` names a type that configures something — an `IConfigureOptions<T>` or an equivalent configurator — and never an extension class. `*Extensions.cs` names ordinary extension methods over a domain or framework type.
* An extension method is declared inside an `extension` block, never with a `this` parameter.

## Architecture layout

* Name a folder by what it is **about**, never by the form of the class it holds (`Services/`, `Utils/`) and never as a catch-all (`Common/`, `Core/`). Class-name suffixes are exempt; the rule governs folders.
* A folder may name a role only when the role is defined in the architecture **and admits exactly one kind of member**.
* Always consider the purpose of a file or folder in a broader context and not just in the current task context before deciding where to create it.
* Depth equals rank. A root folder is something you would name when describing what the project does; a root file is a project-wide thing with no parts.
* Try not to place all files with different purposes in a single directory; analyze whether they can be grouped with other files within the same rank.
* A helper with no standing of its own lives with its consumer and never earns a folder for being a helper. Two consumers move it to the nearest common parent, never to the root.
* A governing subject or a distinct technology keeps its own folder at its own rank, whoever consumes it. Do not collapse *subject* into *consumer*.
* The root of a capability holds its shared vocabulary; subfolders hold its halves. Nest one capability under another only when the product already says so — same data, same route, same name.
* Co-locate what changes together: an options type, its service, and its registration live in one folder.
* One subject keeps one name in every project, and a library puts its front door at its root.
* Every entry point lives in one folder, split by purpose and not by feature.
* If a folder cannot be named without inventing a word, the grouping is wrong. A folder named after a type inside it does not compile from child namespaces.

## Repository map

* `source/RevitLookup` — the production add-in loaded inside `Revit.exe`, and the only project that references the Revit API. Owns the descriptors (`Decomposition/Descriptors`), the Revit-backed service and view-model implementations, and the DI host registration in `Host.cs`.
* `source/RevitLookup.Abstractions` — the shared contracts: view-model interfaces, service interfaces, options, and observable models both hosts implement. No Revit reference.
* `source/RevitLookup.ServiceDefaults` — host configuration shared between the two hosts, and the file-system helpers (`FileSystem/`) the host reads its own paths and permissions through.
* `source/RevitLookup.UI.Framework` — the shared WPF views, controls, converters, and base classes, plus the presentation-level helpers both hosts draw on: `Colors/`, `Processes/`, and the BCL extensions under `Extensions/`.
* `source/RevitLookup.UI.Playground` — the standalone WPF host that mocks the shared contracts under `Mocks/`.
* `source/LookupEngine`, `source/LookupEngine.UI` — git submodules supplying the decomposition engine and its base UI.
* `tests/RevitLookup.Tests` — the TUnit suite that runs inside a Revit process using Nice3point.TUnit.Revit.
* `build/` — the ModularPipelines build.
* `.config/winget/` — the WinGet DSC configuration files listing the per-Revit-year package identifiers.
* Root — `Directory.Build.props`, `Directory.Packages.props`, `global.json`, `GitVersion.yml`, the README, CHANGELOG, wiki sources, CI workflows.

## Build and verify

* Build: `dotnet build --project source/RevitLookup/RevitLookup.csproj -c Release.R##`, where the `R##` suffix is the Revit year (`R27` targets Revit 2027).
* Build the Playground: `dotnet run --project source/RevitLookup.UI.Playground -c Debug`.
* Test: `dotnet test -c Release.R##`; required a matching licensed Revit installation.
