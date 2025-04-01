using System;
using CSPlotlySlim.Chart.Generic.Properties;
using ILNumerics.Drawing;
using DashStyle = CSPlotlySlim.Chart.Generic.Properties.DashStyle;

namespace ILNumerics.Community.WebExport.Generator;

internal static class FormatUtility
{
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

    public static Symbol ToPlotlyMarkerSymbol(this MarkerStyle markerStyle)
    {
        return markerStyle switch
        {
            MarkerStyle.Dot => Symbol.Star,
            MarkerStyle.Circle => Symbol.Circle,
            MarkerStyle.Diamond => Symbol.Diamond,
            MarkerStyle.Square => Symbol.Square,
            MarkerStyle.Rectangle => Symbol.SquareX,
            MarkerStyle.TriangleUp => Symbol.TriangleUp,
            MarkerStyle.TriangleDown => Symbol.TriangleDown,
            MarkerStyle.TriangleLeft => Symbol.TriangleLeft,
            MarkerStyle.TriangleRight => Symbol.TriangleRight,
            MarkerStyle.Plus => Symbol.SquareCross,
            MarkerStyle.Cross => Symbol.Cross,
            MarkerStyle.Custom => Symbol.Star, // Not supported: Fallback to star
            MarkerStyle.None => Symbol.Star,
            _ => throw new ArgumentOutOfRangeException(nameof(markerStyle), markerStyle, null)
        };
    }

    public static string ToMathTex(this string label)
    {
        if (label.IndexOfAny(['{', '}', '_', '^']) != -1)
            return $"${label}$";

        return label;
    }
}