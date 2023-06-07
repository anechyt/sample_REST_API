using Microsoft.AspNetCore.RateLimiting;

namespace SampleRestApi.WebApi.RateLimiting
{
    public static class Extensions
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services)
        {
            return services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(1);
                    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 10;
                });
            });
        }

        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
            => app.UseRateLimiter();
    }
}
