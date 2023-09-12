using System.Threading.RateLimiting;

namespace MiddlewareExplanation.ServicesExtension;

public static class CORSServicesExtension
{
    public static IServiceCollection AddDefaultCorsPolicyServices(this IServiceCollection Services)
    {
        Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });

        return Services;
    }

    public static IServiceCollection AddSpecificCorsPolicyServices(this IServiceCollection Services)
    {
        string MyAllowSpecificOrigins = "_speficCorsPolicy";
        Services.AddCors(options =>
     {
            options.AddPolicy(name:MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://example.com", "http://www.contoso.com")
                                                .WithMethods("PUT", "POST")
                                                .WithHeaders("x-custom-header");
                                  });
        });

        return Services;
    }
}
