namespace CSPlotlySlim.Chart.Generic;

public class Title
{
    public required string Text { get; set; }

    public Font? Font { get; set; }

    public static implicit operator Title(string text) => new Title {Text = text};
}