using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace MiddlewareExplanation.ServicesExtension
{
    public static class RateLimitServicesextension
    {

        // Global Rate Limiter
        public static void AddGlobalRateLimiterServices(this IServiceCollection Services)
        {
            Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                {
                    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            AutoReplenishment = true,
                            QueueLimit = 5,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            Window = TimeSpan.FromSeconds(20)
                        });
                });
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again...", cancellationToken: token);
                };
            });
        }
        // Reate Limiter Algorithm
        public static void AddFixedWindowsRateLimiterServices(this IServiceCollection Services)
        {
            Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;
                options.AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 2;
                    options.Window = TimeSpan.FromSeconds(20);
                    options.AutoReplenishment = true;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                });

            });

        }
        public static void AddSlidingWindowRateLimiterServices(this IServiceCollection Services)
        {
            Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;

                options.AddSlidingWindowLimiter(policyName: "sliding", options =>
                {
                    options.PermitLimit = 30;
                    options.Window = TimeSpan.FromSeconds(60);
                    options.SegmentsPerWindow = 2;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                });
            });
        }
        public static void AddTokenBucketRateLimiterServices(this IServiceCollection Services)
        {
            Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;

                options.AddTokenBucketLimiter(policyName: "token", options =>
                {
                    options.TokenLimit = 10;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                    options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                    options.TokensPerPeriod = 2;
                    options.AutoReplenishment = true;
                });
            });

        }
        public static void AddConcurrencyRateLimiterServices(this IServiceCollection Services)
        {
            Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;
                options.AddConcurrencyLimiter(policyName: "concurrency", options =>
                {
                    options.PermitLimit = 30;
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 5;
                });
            });

        }
    }
}
