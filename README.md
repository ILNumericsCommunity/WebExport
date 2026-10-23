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
- ContourPlot (contour lines; filled regions and labels are not exported)
- Lines, LineStrip and Points (indexed or non-indexed 2D/3D geometry; independent segments retain gaps)
- ErrorBarPlot (vertical/horizontal, asymmetric errors and caps, plus the original line/markers)
- BoxPlot, Candlestick, StackedAreaPlot and FillArea (fills, whiskers, medians and stacked boundaries)
- Triangles, TrianglesStrip and TrianglesFan (2D filled polygons and 3D meshes)
- BarPlot / BarPlotEx (grouped and stacked bars, horizontal and vertical orientation, baselines and custom colors)
- SplinePlot (interpolated curves with original markers)

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

### Contributing

Contributions, bug reports and feature requests are welcome. Please open an issue or a pull request on the GitHub repository.

### License

ILNumerics.Community.WebExport is licensed under the terms of the MIT license (http://opensource.org/licenses/MIT, see LICENSE.txt).
