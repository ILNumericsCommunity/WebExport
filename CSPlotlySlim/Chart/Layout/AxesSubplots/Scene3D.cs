namespace CSPlotlySlim.Chart.Layout.AxesSubplots;

/// <summary>Configures the three Cartesian axes of a Plotly 3D scene.</summary>
public class Scene3D
{
    /// <summary>Gets or sets the X-axis configuration. A null value uses the Plotly default.</summary>
    public XYZAxis? XAxis { get; set; }

    /// <summary>Gets or sets the Y-axis configuration. A null value uses the Plotly default.</summary>
    public XYZAxis? YAxis { get; set; }

    /// <summary>Gets or sets the Z-axis configuration. A null value uses the Plotly default.</summary>
    public XYZAxis? ZAxis { get; set; }
}
