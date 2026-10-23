# WebExport

[![Nuget](https://img.shields.io/nuget/v/ILNumerics.Community.WebExport?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/ILNumerics.Community.WebExport)

Export functionality for ILNumerics (http://ilnumerics.net/) scene graphs and plot cubes to Plotly (interactive data visualization package). WebExport converts ILNumerics scene graphs (for example a PlotCube containing LinePlot objects) into a standalone HTML fragment or file that uses the Plotly JavaScript Open Source Graphing Library to produce interactive charts.

## About Plotly JavaScript Library

Plotly (https://plotly.com/javascript/) is an open source, high-level JavaScript graphing library that produces interactive, publication-quality graphs. It supports many chart types (scatter, line, bar, heatmap, 3D charts, and more) and provides features like hover labels, zooming, panning, and responsive layouts. WebExport produces Plotly-compatible markup so the resulting HTML can be embedded in a web page or opened directly to present an interactive chart.

## Features

- Convert ILNumerics scene graphs / plot cubes into Plotly charts
- Output as a standalone HTML string or write directly to a file

**Supported plot types**

The following plot types are supported:

- LinePlot (2D and 3D)
- Surface (rectilinear grids)
- FastSurface (rectilinear grids)
- ImageSCPlot (scalar heatmaps, linear 2D axes, color limits and sampled colormap)
- ContourPlot (existing contour-line geometry; filled regions and labels are reported as omitted)
- Lines, LineStrip and Points (indexed or non-indexed 2D/3D geometry; independent segments retain gaps)
- ErrorBarPlot (vertical/horizontal, asymmetric errors and caps, plus the original line/markers)
- BoxPlot, Candlestick, StackedAreaPlot and FillArea (existing calculated geometry; includes fills, whiskers, medians and stacked boundaries)
- Triangles, TrianglesStrip and TrianglesFan (explicit 2D filled polygons or 3D meshes; indexed buffers and NaN separators)
- BarPlot / BarPlotEx (headless geometry preparation, including grouping/stacking, orientation, baseline, padding and color overrides)
- SplinePlot (native not-a-knot interpolation computed headlessly, with original markers)

## Getting started

Install the NuGet package:

```powershell
dotnet add package ILNumerics.Community.WebExport
```

## Basic usage examples

Two main entry points are provided:

- ExportString(scene) - returns a complete HTML document as a string
- ExportFile(scene, filePath) - writes the HTML to a file (usually with .html extension)

### Examples

1) Export scene to a string
   
   ```csharp
   // Create or obtain an ILNumerics scene (pseudocode)
   // var scene = new Scene() { ... };
   
   string html = WebExport.ExportString(scene);
   ```

2) Export scene to a file
   
   ```csharp
   string filePath = "webplot.html";
   WebExport.ExportFile(scene, filePath);
   // The file 'webplot.html' now contains the HTML page
   ```

## Export diagnostics and offline HTML

```csharp
using CSPlotlySlim.Chart;

ExportResult result = WebExport.ExportWithReport(scene, HtmlRenderMode.Offline);
foreach (ExportIssue issue in result.Issues)
    Console.WriteLine($"Cube {issue.PlotCubeIndex}: {issue.NodeType}: {issue.Message}");

WebExport.ExportFile(scene, "offline.html", HtmlRenderMode.Offline);
```

`ExportResult.Html` includes visible omission/approximation notices as well as supported charts. `Issues` is read-only; cube indices are zero-based, and -1 denotes scene-level content. Empty or unsupported cubes retain their placeholder. Unexpected exceptions still propagate.

The existing one-argument APIs retain CDN behavior. `Chart.Render` and `RenderPartial` also accept `HtmlRenderMode`: `Cdn`, `Offline`, or `Existing` (the containing page provides Plotly). A multi-cube offline document embeds the library once. Offline HTML is larger by about 5 MB and requires inline JavaScript to be allowed by the hosting page's CSP.

The embedded Plotly 3.3.1 bundle comes from [the pinned upstream distribution](https://github.com/plotly/plotly.js/blob/v3.3.1/dist/plotly.min.js), with its MIT license in `CSPlotlySlim/Assets/Plotly-LICENSE.txt`. SHA-256: `E2B0F77EE8156FF9D525852FE4ACB7BFE7D85E8A2E1DA0523F6D16209B848968`. Updating it is an explicit dependency update; exports never download or cache assets on the .NET side.

## Notes and limitations

- Empty scenes and plots without supported visible data produce a visible HTML placeholder. They no longer produce empty strings or empty files.
- `ExportString` and `ExportFile` export all plot cubes sequentially. The original viewport arrangement is not retained. `GetChart` retains its first-plot-cube behavior and returns null when that cube has no supported data.
- Unsupported plot types (including custom plot groups) are reported and omitted rather than exported as incomplete internal line objects. Transparent `Group` containers are traversed. Transformed plot groups are reported as unsupported. Invalid vertex indices throw rather than silently drawing incorrect data.
- Scene lighting and screen overlays are not exported. Diagnostics cover plot content and unsupported content outside plot cubes; they do not claim pixel-equivalent rendering. Per-vertex line/point colors are reported as approximated by one trace color.
- Statistical and area plots are exported as geometry, not native Plotly statistical traces: hover statistics, category labels and legend semantics are not reconstructed. This preserves ILNumerics' calculated geometry rather than recalculating quartiles or stacking in Plotly.
- Bar and spline geometry is prepared on isolated copies before computing export bounds; the original scene is not configured or mutated.
- 2D triangle fills use separate traces to preserve overlapping polygons. Large triangulated scenes can therefore create many traces. Per-vertex triangle colors are approximated by a flat face color and reported; custom lighting is not replicated.
- Export uses ILNumerics 7.4.62. Accelerator rewriting is disabled for the export layer to avoid coupling exported assemblies to generated accelerator internals. Consumers should align their ILNumerics packages.
- The default mode loads Plotly and optional MathJax from CDNs. Offline mode embeds Plotly and makes no library network requests for the supported traces. MathJax is not bundled: offline math labels remain plain text. Loading or rendering failures show a visible message.
- Non-finite numeric values become JSON null (gaps). Unexpected conversion, serialization, and file I/O exceptions propagate to the caller.
- Export on the scene's owning thread, without concurrent scene mutation.

## Validation

Run the headless regression suite from the repository root (requires .NET 10 and a valid ILNumerics license):

```powershell
dotnet run --project WebExport.Tests -c Release -p:SolutionDir="$PWD/"
```

The tests cover original line/surface behavior, scalar-image orientation and values, contour geometry, independent line segments, hidden groups, partial-export diagnostics, and offline packaging.

Optional real-browser validation (Node.js and Playwright required):

```powershell
$env:WEBEXPORT_OFFLINE_SMOKE_PATH = "$PWD/offline-smoke.html"
dotnet run --project WebExport.Tests -c Release -p:SolutionDir="$PWD/"
# Install Playwright and its Chromium browser in your test environment first.
node WebExport.Tests/BrowserSmoke.cjs $env:WEBEXPORT_OFFLINE_SMOKE_PATH

# Additional standard-plot fixture and browser checks:
$env:WEBEXPORT_STANDARD_SMOKE_PATH = "$PWD/standard-smoke.html"
dotnet run --project WebExport.Tests -c Release -p:SolutionDir="$PWD/"
node WebExport.Tests/BrowserSmoke.cjs $env:WEBEXPORT_STANDARD_SMOKE_PATH --standard
```

`WEBEXPORT_BROWSER_PATH` can select an installed Chromium/Edge executable. The smoke test disables networking, checks actual rendered trace types and JavaScript errors, verifies asymmetric error/statistical/area/mesh rendering in the standard fixture, checks candlestick fill coverage, and verifies asynchronous rendering failures produce a visible message. The optional fixture path is only used by tests.

### Contributing

Contributions, bug reports and feature requests are welcome. Please open an issue or a pull request on the GitHub repository.

### License

ILNumerics.Community.WebExport is licensed under the terms of the MIT license (http://opensource.org/licenses/MIT, see LICENSE.txt).

## Shared export samples

`Shared/ExportDemoScenes.cs` supplies the same 23 deterministic standard ILNumerics scenes to WebExport and TikzExport demos and headless tests. Run `Shared/VerifySamples.ps1 -OtherRepository ../TikzExport` after changes to keep their sources identical. Both repositories build independently; see `Shared/README.md`.

## Headless bar and spline preparation

Both exporters compile the identical `Shared/HeadlessPlotPreparation.cs` adapter and run identical `Shared/PreparationTests.cs` tests. Source buffers are explicitly detached, so preparation, disposal, and repeated exports do not change the caller's plot. Bounds include the prepared geometry. Bar border colors are grouped into independent line batches; TikzExport batches 3D faces to avoid excessive TeX memory consumption.

Explicit `SplinePlot.Resolution` is honored. When it is null, export uses **32** as the native sampling factor (ILNumerics 7.4.62 produces `controlPointCount * resolution` samples across the curve). This is deterministic rather than dependent on viewport pixels. Native chord-length and 1D interpolation modes are retained; original data markers remain at the control points. Invalid interpolation data propagates an exception, never a control-polygon fallback.

Compatibility: ILNumerics 7.4.62 keeps spline tessellation and some bar settings private. The shared adapter uses a small reflection bridge to invoke its native math and preserve state omitted by native copies, with explicit errors if the required members disappear. The adapter also repairs native grouped-row offsets and the ignored stacked baseline; tests check expected coordinates independently of the native implementation. Bar calculation runs only on detached nodes, bypassing axis/font creation. Nullable per-bar/per-group color overrides are applied independently because the native implementation dereferences both optional colors. This bridge requires review when upgrading ILNumerics and is not intended for trimming/AOT without preserving these members. A public upstream `PrepareGeometry` API would remove that dependency.

Automatic ticks/legends and other general export limitations still apply; preparation reconstructs geometry, not native UI layout. Desktop UI rendering is a separate validation path.

## Release validation

Build the solution in Release and run the headless test executable:

```powershell
dotnet build WebExport.slnx -c Release
dotnet run --project WebExport.Tests -c Release
dotnet list WebExport.slnx package --outdated
./Shared/VerifySamples.ps1 -OtherRepository ../TikzExport
```

Public API documentation warnings fail the build. Apply JetBrains `Built-in: Careful Cleanup` using the solution settings, then review the diff and rerun both exporter suites. Shared conditional sources must retain imports required by both exporters; keep their three shared files byte-identical after cleanup.

Release tags use `vMAJOR.MINOR.PATCH`. The existing package convention appends a date-based build number, so tag `v7.2.0` produces package version `7.2.0.<build-number>`. Build and pack after tagging to embed the release version and source revision. Build-time analyzers and MinVer are private dependencies.
