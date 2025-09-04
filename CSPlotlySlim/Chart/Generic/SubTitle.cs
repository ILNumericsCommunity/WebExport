using CSPlotlySlim.Chart.Layout.Properties;

namespace CSPlotlySlim.Chart.Generic;

public class SubTitle
{
    public required string Text { get; set; }

    public Font? Font { get; set; }

    public float? X { get; set; }

    public XAnchor? XAnchor { get; set; }

    public float? Y { get; set; }

    public YAnchor? YAnchor { get; set; }

    public static implicit operator SubTitle(string text) => new SubTitle {Text = text};
}