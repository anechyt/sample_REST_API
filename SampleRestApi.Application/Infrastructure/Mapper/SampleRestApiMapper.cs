using Riok.Mapperly.Abstractions;
using SampleRestApi.Application.Dogs.Commands.CreateDog;
using SampleRestApi.Application.Dogs.Models;
using SampleRestApi.Domain.Entities;

namespace SampleRestApi.Application.Infrastructure.Mapper
{
    [Mapper]
    public partial class SampleRestApiMapper
    {
        public partial Dog CreateDogCommandMapper(CreateDogRequest createDogRequest);
        public partial DogModel CreateDogResponseMapper(Dog dog);

        public partial List<DogModel> GetDogsRequestMapper(List<Dog> dogs);
    }
}
