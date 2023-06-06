using Microsoft.EntityFrameworkCore;
using SampleRestApi.Domain.Entities;

namespace SampleRestApi.Persistence
{
    public class SampleRestApiContext : DbContext
    {
        public SampleRestApiContext(DbContextOptions<SampleRestApiContext> contextOptions) : base(contextOptions) { }

        public virtual DbSet<Dog> Dogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
