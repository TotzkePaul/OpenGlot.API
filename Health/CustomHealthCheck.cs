using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PolyglotAPI.Health
{
    public class CustomHealthCheck : IHealthCheck
    {
        private static int counter = 0;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Perform custom health check logic here
            bool healthCheckResultHealthy = true; // Replace with your actual check
            counter++;

            if (healthCheckResultHealthy && counter % 10 !=9)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The custom check is healthy."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The custom check is unhealthy."));
        }
    }

}
