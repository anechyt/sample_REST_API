using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleRestApi.Domain.Entities;

namespace SampleRestApi.Persistence.Configuration
{
    public class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.HasKey(e => e.Gid);

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Color).HasMaxLength(50).IsRequired();
            builder.Property(e => e.TailLength).IsRequired();
            builder.Property(e => e.Weight).IsRequired();
        }
    }
}
