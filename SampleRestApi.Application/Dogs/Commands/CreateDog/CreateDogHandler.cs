using Mediator;
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

            var dog = mapper.CreateDogCommandMapper(command);

            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();

            var dogModel = mapper.CreateDogResponseMapper(dog);

            return dogModel;
        }
    }
}
