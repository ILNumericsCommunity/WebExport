using System;
using System.Windows.Forms;
using CSPlotlySlim.Chart;
using CSPlotlySlim.Chart.Layout;
using CSPlotlySlim.Chart.Layout.AxesSubplots;
using CSPlotlySlim.Chart.Traces;
using CSPlotlySlim.Chart.Traces.Properties;

namespace WebExportDemo;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Test();

        Application.EnableVisualStyles();
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.SetCompatibleTextRenderingDefault(false);

        Application.Run(new WebPlotDemoForm());
    }

    private static void Test()
    {
        var layout = new Layout
        {
            Title = "Line chart",
            XAxis = new XYZAxis
            {
                Title = "X Axis"
            },
            YAxis = new XYZAxis
            {
                Title = "Y Axis"
            }
        };

        var trace1 = new ScatterTrace
        {
            Mode = ScatterMode.Lines,
            X = [1, 2, 3, 4],
            Y = [10, 15, 13, 17],
            //Marker = new Marker
            //{
            //    Color = Color.Red
            //},
            //TextFont = new Font()
            //{
            //    Color = Color.Blue,
            //    Family = "Arial",
            //    Style = FontStyle.Normal,
            //    Size = 12,
            //    Weight = 400,
            //    Case = TextCase.Normal,
            //    Variant = TextVariant.Normal
            //}
        };

        var chart = new Chart(layout, trace1);
        
        var html = chart.Render("myDiv");
    }
}