namespace CSPlotlySlim.Chart;

/// <summary>How the HTML obtains its JavaScript libraries.</summary>
public enum HtmlRenderMode
{
    /// <summary>Load Plotly and optional MathJax from the CDN.</summary>
    Cdn,

    /// <summary>Embed the pinned Plotly bundle. No MathJax or network loader is included.</summary>
    Offline,

    /// <summary>Use Plotly already provided by the containing document.</summary>
    Existing
}
