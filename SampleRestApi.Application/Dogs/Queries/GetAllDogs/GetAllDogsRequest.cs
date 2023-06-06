using Mediator;
using SampleRestApi.Application.Dogs.Models;
using SampleRestApi.Application.Models;

namespace SampleRestApi.Application.Dogs.Queries.GetAllDogs
{
    public class GetAllDogsRequest : IRequest<DataResponseModel<DogModel>> 
    {
        public string? Attribute { get; set; }

        public string? Order { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
