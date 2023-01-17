namespace Containers.Common.HealthCheck.Checks;

public class HealthCheckParam
{
    public string? Title { get; set; }
    public string? Host { get; set; }
    public int HealthyRoundtripTime { get; set; }
}
