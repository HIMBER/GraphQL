using System.Diagnostics;

namespace PocGraphQL.Common.Telemetry;

public class DiagnosticConfig
{
    public const string SourceName = "UserService";
    public  ActivitySource Source = new ActivitySource(SourceName);
}