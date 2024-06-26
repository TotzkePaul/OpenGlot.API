using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.XPath;

namespace PolyglotAPI.Common
{
    public class HealthCheckOperationFilter : IDocumentFilter
    {
        public static string HealthCheckEndpoint = "/api/health";

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths.Add(HealthCheckEndpoint, new OpenApiPathItem
            {
                Operations = new Dictionary<OperationType, OpenApiOperation>
                {
                    [OperationType.Get] = new OpenApiOperation
                    {
                        Summary = "Health Check Endpoint",
                        Description = "Provides a health status of the application.",
                        Responses = new OpenApiResponses
                        {
                            ["200"] = new OpenApiResponse { Description = "Healthy" },
                            ["503"] = new OpenApiResponse { Description = "Unhealthy" }
                        }
                    }
                }
            });
        }
    }
}
