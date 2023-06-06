using FluentValidation;
using SampleRestApi.Application.Dogs.Commands.CreateDog;

namespace SampleRestApi.Application.Dogs.Validators
{
    public class CreateDogValidator : AbstractValidator<CreateDogRequest>
    {
        public CreateDogValidator()
        {
            RuleFor(dog => dog.Name).NotNull().MaximumLength(50);
            RuleFor(dog => dog.Color).NotNull().MaximumLength(50);
            RuleFor(dog => dog.TailLength).NotEmpty().GreaterThan(0);
            RuleFor(dog => dog.Weight).NotEmpty().GreaterThan(0);
        }
    }
}
