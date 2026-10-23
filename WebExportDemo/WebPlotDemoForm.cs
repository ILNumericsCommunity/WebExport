using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ILNumerics.Community.ExportSamples;
using ILNumerics.Community.WebExport;
using ILNumerics.Drawing;

namespace WebExportDemo;

/// <summary>Displays standard ILNumerics scenes and their HTML exports.</summary>
public partial class WebPlotDemoForm : Form
{
    /// <summary>Initializes the demo controls and shared scene selector.</summary>
    public WebPlotDemoForm()
    {
        InitializeComponent();

        comboBoxScene.DataSource = ExportDemoScenes.Names;
    }

    private async void XPlotDemoForm_Load(object? sender, EventArgs e)
    {
        comboBoxScene.SelectedIndex = 0;

        try
        {
            await webView.EnsureCoreWebView2Async();
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message, "Browser initialization failed");
        }
    }

    private void comboBoxScene_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (comboBoxScene.SelectedIndex == -1)
            return;

        var previousScene = ilPanel.Scene;
        ilPanel.Scene = ExportDemoScenes.Create(comboBoxScene.SelectedIndex);
        previousScene?.Dispose();

        ilPanel.Configure();
        ilPanel.Refresh();
    }

    private string? exportFile;

    private async void btnExportPlotly_Click(object? sender, EventArgs e)
    {
        try
        {
            await webView.EnsureCoreWebView2Async();
            var currentScene = ilPanel.GetCurrentScene() ?? ilPanel.Scene;
            var html = WebExport.ExportString(currentScene);
            exportFile ??= Path.Combine(Path.GetTempPath(), $"ilnumerics-export-{Guid.NewGuid():N}.html");
            File.WriteAllText(exportFile, html, Encoding.UTF8);
            webView.CoreWebView2.Navigate(new Uri(exportFile).AbsoluteUri);
        }
        catch (Exception error)
        {
            MessageBox.Show(this, error.Message, "HTML export failed");
        }
    }

    /// <inheritdoc />
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        if (exportFile == null)
            return;
        try
        {
            File.Delete(exportFile);
        }
        catch (IOException)
        {
            /* The browser may still be releasing its file handle. */
        }
    }

    #region Private

    /// <summary>Renders the scene as SVG markup using an off-screen SVG driver.</summary>
    /// <param name="scene">Scene to render on its owning thread.</param>
    /// <returns>SVG markup without the XML declaration or document type.</returns>
    public static string WriteSVG(Scene scene)
    {
        using (var memoryStream = new MemoryStream())
        {
            // Render SVG into memory stream
            new SVGDriver(memoryStream, 800, 600, scene).Render();

            var svgBytes = memoryStream.ToArray();
            var svgContent = Encoding.UTF8.GetString(svgBytes, 0, svgBytes.Length);

            svgContent = svgContent.Replace("<?xml version='1.0' encoding='UTF-8'?>", ""); // Strip XML header
            svgContent = svgContent.Replace("<!DOCTYPE svg PUBLIC '-//W3C//DTD SVG 1.1//EN' 'http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd'>", ""); // Strip DocType header
            svgContent = svgContent.Replace("\r\n", ""); // Strip all line breaks

            return svgContent;
        }
    }

    #endregion
}
