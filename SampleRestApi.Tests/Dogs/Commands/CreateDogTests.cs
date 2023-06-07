using SampleRestApi.Application.Dogs.Commands.CreateDog;
using SampleRestApi.Application.Dogs.Models;
using SampleRestApi.Tests.Helpers;

namespace SampleRestApi.Tests.Dogs.Commands
{
    public abstract class CreateDogTests
    {
        public abstract class CreateDogRequestTests : BaseTest
        {
            protected readonly CreateDogRequest _dogRequest;

            protected readonly CreateDogHandler _dogHandler;

            protected CreateDogRequestTests()
            {
                _dogRequest = new CreateDogRequest()
                {
                    Name = "Test",
                    Color = "TestColor",
                    TailLength = 10,
                    Weight = 10,
                };

                _dogHandler = new CreateDogHandler(_dbContext);
            }
        }

        public class Handler : CreateDogRequestTests
        {
            [Fact]
            public async Task Dog_model_is_returned_when_request_is_valid()
            {
                var expectedDog = new DogModel
                {
                    Name = "Test",
                    Color = "TestColor",
                    TailLength = 10,
                    Weight = 10,
                };
                var result = await _dogHandler.Handle(_dogRequest, new CancellationToken());

                result.Should().BeEquivalentTo(expectedDog);
            }

            [Fact]
            public async Task Bad_request_is_returned_when_request_is_invalid()
            {
                _dogRequest.Name = string.Empty;

                var result = await _dogHandler.Handle(_dogRequest, new CancellationToken());

                result.Should().NotBeNull();
            }
        }
    }
}
