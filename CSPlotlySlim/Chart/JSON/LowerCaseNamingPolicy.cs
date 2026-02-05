using System;
using System.Text;
using System.Text.Json;

namespace CSPlotlySlim.Chart.JSON;

/// <summary>
/// Converts property names to the Plotly-compatible lowercase format while preserving underscores.
/// </summary>
public sealed class PlotlyNamingPolicy : JsonNamingPolicy
{
    /// <inheritdoc />
    public override string ConvertName(string name)
    {
        if (String.IsNullOrEmpty(name))
            return name;

        var builder = new StringBuilder(name.Length);
        var previousUnderscore = false;

        foreach (var ch in name)
        {
            if (ch == '_')
            {
                if (!previousUnderscore)
                {
                    builder.Append('_');
                    previousUnderscore = true;
                }

                continue;
            }

            previousUnderscore = false;
            builder.Append(Char.ToLowerInvariant(ch));
        }

        return builder.ToString();
    }
}
