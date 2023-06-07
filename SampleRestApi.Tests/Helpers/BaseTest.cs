using Microsoft.EntityFrameworkCore;
using SampleRestApi.Domain.Entities;
using SampleRestApi.Persistence;

namespace SampleRestApi.Tests.Helpers
{
    public class BaseTest : IDisposable
    {
        protected readonly SampleRestApiContext _dbContext;

        public BaseTest()
        {
            _dbContext = InitTestDbContext();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        public SampleRestApiContext InitTestDbContext()
        {
            var options = new DbContextOptionsBuilder<SampleRestApiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SampleRestApiContext(options);
            context.Database.EnsureCreated();

            SeedDb(context);
            context.SaveChanges();

            return context;
        }

        private void SeedDb(SampleRestApiContext context)
        {
            if (!context.Dogs.Any())
            {
                var dogs = new List<Dog>()
                {
                    new Dog
                    {
                        Name = "TestName",
                        Color = "TestColor",
                        TailLength = 10,
                        Weight = 10,
                    },
                    new Dog
                    {
                        Name = "TestSecondName",
                        Color = "TestColor",
                        TailLength = 154,
                        Weight = 123,
                    }
                };

                context.AddRange(dogs);
                context.SaveChanges();
            }
        }
    }
}
