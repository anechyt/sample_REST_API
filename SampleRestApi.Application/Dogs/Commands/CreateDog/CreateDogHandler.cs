using Mediator;
using Microsoft.EntityFrameworkCore;
using SampleRestApi.Application.Dogs.Models;
using SampleRestApi.Application.Infrastructure.Mapper;
using SampleRestApi.Persistence;

namespace SampleRestApi.Application.Dogs.Commands.CreateDog
{
    public class CreateDogHandler : ICommandHandler<CreateDogRequest, DogModel>
    {
        private readonly SampleRestApiContext _context;

        public CreateDogHandler(SampleRestApiContext context)
        {
            _context = context;
        }

        public async ValueTask<DogModel> Handle(CreateDogRequest command, CancellationToken cancellationToken)
        {
            var mapper = new SampleRestApiMapper();

            var existingdog = await _context.Dogs.AsNoTracking().Where(x => x.Name == command.Name).FirstOrDefaultAsync(cancellationToken);

            var dog = mapper.CreateDogCommandMapper(command);

            if (existingdog is null)
            {
                await _context.Dogs.AddAsync(dog, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception($"Dog with name {command.Name} already exists in DB.");
            }

            var dogModel = mapper.CreateDogResponseMapper(dog);

            return dogModel;
        }
    }
}
