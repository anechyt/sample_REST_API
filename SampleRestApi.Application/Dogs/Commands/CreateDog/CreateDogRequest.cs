using Mediator;
using SampleRestApi.Application.Dogs.Models;

namespace SampleRestApi.Application.Dogs.Commands.CreateDog
{
    public class CreateDogRequest : ICommand<DogModel>
    {
        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;

        public int TailLength { get; set; }

        public int Weight { get; set; }
    }
}
