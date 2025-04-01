namespace CSPlotlySlim.Chart.Generic;

public class LegendTitle : Title
{
    public SubTitle? SubTitle { get; set; }

    public static implicit operator LegendTitle(string text) => new LegendTitle {Text = text};
}