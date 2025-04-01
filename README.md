WebExport
==========
[![Nuget](https://img.shields.io/nuget/v/ILNumerics.Community.WebExport?style=flat-square&logo=nuget&color=blue)](https://www.nuget.org/packages/ILNumerics.Community.WebExport)

Export functionality for ILNumerics (http://ilnumerics.net/) scene graphs and plot cubes to [Plotly](https://plotly.com/) (interactive data visualization package).

## How to use

Export scene to interactive web chart:
```csharp
var webChart = WebExport.Export(scene);
```
As of today (March 2025) only _LinePlot_ is supported.

### License
ILNumerics.Community.WebExport is licensed under the terms of the MIT license (<http://opensource.org/licenses/MIT>, see LICENSE.txt).
