using System.Collections.Generic;

namespace ILNumerics.Community.WebExport;

/// <summary>An omitted plot or export approximation. PlotCubeIndex is zero-based, or -1 for the scene.</summary>
/// <param name="PlotCubeIndex">Zero-based cube index, or -1 for a scene-level issue.</param>
/// <param name="NodeType">Native scene-node type associated with the issue.</param>
/// <param name="Message">Human-readable explanation of omitted or approximated content.</param>
public sealed record ExportIssue(int PlotCubeIndex, string NodeType, string Message);

/// <summary>HTML together with explicit conversion diagnostics. Exceptions are not suppressed.</summary>
/// <param name="Html">Generated markup, including placeholders when conversion is unavailable.</param>
/// <param name="Issues">Conversion limitations associated with this snapshot.</param>
public sealed record ExportResult(string Html, IReadOnlyList<ExportIssue> Issues);
