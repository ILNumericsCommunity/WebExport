using System.Text.Json;

namespace CSPlotlySlim.Chart.JSON;

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToLower();
}