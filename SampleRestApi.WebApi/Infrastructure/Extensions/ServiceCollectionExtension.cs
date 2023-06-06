using Microsoft.EntityFrameworkCore;
using SampleRestApi.Persistence;

namespace SampleRestApi.WebApi.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static async Task<IServiceCollection> AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["Db:DefaultConnectionString"];
            services.AddDbContext<SampleRestApiContext>(options =>
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(SampleRestApiContext).Assembly.FullName)));

            return services;
        }
    }
}
