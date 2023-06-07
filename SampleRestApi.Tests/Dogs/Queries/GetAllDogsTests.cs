using SampleRestApi.Application.Dogs.Queries.GetAllDogs;
using SampleRestApi.Tests.Helpers;

namespace SampleRestApi.Tests.Dogs.Queries
{
    public abstract class GetAllDogsTests
    {
        public abstract class GetAllDogsRequestTest : BaseTest
        {
            protected readonly GetAllDogsRequest _allDogsRequest;

            protected readonly GetAllDogsHandler _allDogsHandler;

            protected GetAllDogsRequestTest()
            {
                _allDogsRequest = new GetAllDogsRequest();

                _allDogsHandler = new GetAllDogsHandler(_dbContext);
            }
        }

        public class Handle : GetAllDogsRequestTest
        {
            [Fact]
            public async Task DogsList_is_returned_when_request_is_valid()
            {
                var result = await _allDogsHandler.Handle(_allDogsRequest, new CancellationToken());

                result.Items.Should().HaveCount(x => x >= 1);
            }
        }
    }
}
