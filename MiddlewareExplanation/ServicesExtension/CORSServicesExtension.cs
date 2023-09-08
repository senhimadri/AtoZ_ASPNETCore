using System.Threading.RateLimiting;

namespace MiddlewareExplanation.ServicesExtension;

public static class CORSServicesExtension
{
    public static void AddDefaultCorsPolicyServices(this IServiceCollection Services)
    {
        Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });
    }

    public static void AddSpecificCorsPolicyServices(this IServiceCollection Services)
    {
        string MyAllowSpecificOrigins = "_speficCorsPolicy";
        Services.AddCors(options =>
        {
            options.AddPolicy(name:MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://example.com", "http://www.contoso.com");
                                      policy.WithMethods();
                                      policy.WithHeaders();
                                  }) ;
        });
    }
}
