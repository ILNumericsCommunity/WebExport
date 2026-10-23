using System;
using CSPlotlySlim.Chart.Generic.Properties;
using ILNumerics.Drawing;
using DashStyle = CSPlotlySlim.Chart.Generic.Properties.DashStyle;

namespace ILNumerics.Community.WebExport.Generator;

/// <summary>Converts supported scene geometry into an export snapshot without a UI.</summary>
internal static class FormatUtility
{
    /// <summary>Maps native line dashes to supported Plotly dash patterns.</summary>
    public static DashStyle ToPlotlyDashStyle(this Drawing.DashStyle dashStyle)
    {
        return dashStyle switch
        {
            Drawing.DashStyle.Solid => DashStyle.Solid,
            Drawing.DashStyle.Dashed => DashStyle.Dash,
            Drawing.DashStyle.PointDash => DashStyle.DashDot,
            Drawing.DashStyle.Dotted => DashStyle.Dot,
            Drawing.DashStyle.UserPattern => DashStyle.Solid, // Not supported: Fallback to solid
            _ => throw new ArgumentOutOfRangeException(nameof(dashStyle), dashStyle, null)
        };
    }

    /// <summary>Maps native marker shapes to Plotly symbols.</summary>
    public static Symbol ToPlotlyMarkerSymbol(this MarkerStyle markerStyle)
    {
        return markerStyle switch
        {
            MarkerStyle.Dot => Symbol.Circle,
            MarkerStyle.Circle => Symbol.Circle,
            MarkerStyle.Diamond => Symbol.Diamond,
            MarkerStyle.Square => Symbol.Square,
            MarkerStyle.Rectangle => Symbol.Square,
            MarkerStyle.TriangleUp => Symbol.TriangleUp,
            MarkerStyle.TriangleDown => Symbol.TriangleDown,
            MarkerStyle.TriangleLeft => Symbol.TriangleLeft,
            MarkerStyle.TriangleRight => Symbol.TriangleRight,
            MarkerStyle.Plus => Symbol.Cross,
            MarkerStyle.Cross => Symbol.X,
            MarkerStyle.Custom => Symbol.Star, // Not supported: Fallback to star
            MarkerStyle.None => Symbol.Star,
            _ => throw new ArgumentOutOfRangeException(nameof(markerStyle), markerStyle, null)
        };
    }

    /// <summary>Wraps labels containing mathematical syntax for MathJax rendering.</summary>
    public static string ToMathTex(this string label)
    {
        if (label.IndexOfAny(['{', '}', '_', '^']) != -1)
            return $"${label}$";

        return label;
    }
}
