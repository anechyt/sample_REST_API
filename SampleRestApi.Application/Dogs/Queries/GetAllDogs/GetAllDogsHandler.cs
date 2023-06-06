using Azure.Core;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SampleRestApi.Application.Dogs.Models;
using SampleRestApi.Application.Infrastructure.Mapper;
using SampleRestApi.Application.Models;
using SampleRestApi.Domain.Entities;
using SampleRestApi.Persistence;

namespace SampleRestApi.Application.Dogs.Queries.GetAllDogs
{
    public class GetAllDogsHandler : IRequestHandler<GetAllDogsRequest, DataResponseModel<DogModel>>
    {
        private readonly SampleRestApiContext _context;

        public GetAllDogsHandler(SampleRestApiContext context)
        {
            _context = context;
        }

        public async ValueTask<DataResponseModel<DogModel>> Handle(GetAllDogsRequest request, CancellationToken cancellationToken)
        {
            var (filters, pagination) = IsRequestHaveFilters(request);

            if (filters && !pagination)
            {
                return await GetDogsListByFiltersAsync(request, cancellationToken);
            }
            if (pagination && !filters)
            {
                return await GetDogsListByPaginationAsync(request, null, cancellationToken);
            }
            if (!filters && !pagination)
            {
                return await GetDogsListAsync(cancellationToken);
            }
            if (filters && pagination)
            {
                var filtersResult = await GetDogsListByFiltersAsync(request, cancellationToken);
                return await GetDogsListByPaginationAsync(request, filtersResult, cancellationToken);
            }

            return new DataResponseModel<DogModel>();
        }

        private (bool, bool) IsRequestHaveFilters(GetAllDogsRequest request)
        {
            if (request.Attribute is null &&
                request.Order is null &&
                request.PageNumber is null &&
                request.PageSize is null)
            {
                return (false, false);
            }
            if (request.Attribute is not null &&
                request.Order is not null &&
                request.PageNumber is null &&
                request.PageSize is null)
            {
                return (true, false);
            }
            if (request.Attribute is null &&
                request.Order is null &&
                request.PageNumber is not null &&
                request.PageSize is not null)
            {
                return (false, true);
            }
            else
            {
                return (true, true);
            }
        }

        private async ValueTask<DataResponseModel<DogModel>> GetDogsListByPaginationAsync(GetAllDogsRequest request, DataResponseModel<DogModel>? filtersResponse, CancellationToken cancellationToken)
        {
            var mapper = new SampleRestApiMapper();

            if (filtersResponse is not null)
            {
                int totalItems = filtersResponse.Items.Count();
                var result = Pagination<DogModel>(request, totalItems, filtersResponse.Items);

                return new DataResponseModel<DogModel> { Items = result };
            }
            else
            {
                var dogs = await _context.Dogs.AsNoTracking().ToListAsync(cancellationToken);

                int totalItems = dogs.Count();
                var result = Pagination<Dog>(request, totalItems, dogs);

                var items = mapper.GetDogsRequestMapper(result.ToList());
                return new DataResponseModel<DogModel> { Items = items };
            }
        }

        private List<T> Pagination<T>(GetAllDogsRequest request, int totalItems, List<T> entities) where T : class
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)request.PageSize);

            if (request.PageNumber < 1 || request.PageNumber > totalPages)
            {
                throw new Exception("Invalid page number specified.");
            }
            var skip = (request.PageNumber - 1) * request.PageSize;
            var pageSize = request.PageSize;

            var result = entities.Skip((int)skip).Take((int)pageSize);

            return result.ToList();
        }

        private async ValueTask<DataResponseModel<DogModel>> GetDogsListAsync(CancellationToken cancellationToken)
        {
            var mapper = new SampleRestApiMapper();

            var dogs = await _context.Dogs.AsNoTracking().ToListAsync(cancellationToken);

            var items = mapper.GetDogsRequestMapper(dogs);

            return new DataResponseModel<DogModel> { Items = items };
        }


        private async ValueTask<DataResponseModel<DogModel>> GetDogsListByFiltersAsync(GetAllDogsRequest request, CancellationToken cancellationToken)
        {
            var mapper = new SampleRestApiMapper();

            if (string.IsNullOrEmpty(request.Attribute) || string.IsNullOrEmpty(request.Order))
            {
                throw new Exception("Request model is empty");
            }

            IQueryable<Dog> query = _context.Dogs.AsNoTracking();

            switch (request.Attribute)
            {
                case "name":
                    query = OrderByName(request, query);
                    break;

                case "weight":
                    query = OrderByWeight(request, query);
                    break;

                case "tail":
                    query = OrderByTailLength(request, query);
                    break;

                case "color":
                    query = OrderByColor(request, query);
                    break;

                default:
                    throw new Exception("Invalid attribute specified.");
            }

            var dogModels = mapper.GetDogsRequestMapper(await query.ToListAsync(cancellationToken));

            return new DataResponseModel<DogModel> { Items = dogModels };
        }

        private IQueryable<Dog> OrderByColor(GetAllDogsRequest request, IQueryable<Dog> query)
        {
            return (request.Order.ToLower() == "desc") ? query.OrderByDescending(x => x.Color)
                        : query = (request.Order.ToLower() == "asc") ? query.OrderBy(x => x.Color)
                        : throw new Exception("Invalid order specified.");
        }

        private IQueryable<Dog> OrderByTailLength(GetAllDogsRequest request, IQueryable<Dog> query)
        {
            return (request.Order.ToLower() == "desc") ? query.OrderByDescending(x => x.TailLength)
                        : query = (request.Order.ToLower() == "asc") ? query.OrderBy(x => x.TailLength)
                        : throw new Exception("Invalid order specified.");
        }

        private IQueryable<Dog> OrderByWeight(GetAllDogsRequest request, IQueryable<Dog> query)
        {
            return (request.Order.ToLower() == "desc") ? query.OrderByDescending(x => x.Weight)
                        : query = (request.Order.ToLower() == "asc") ? query.OrderBy(x => x.Weight)
                        : throw new Exception("Invalid order specified.");
        }

        private IQueryable<Dog> OrderByName(GetAllDogsRequest request, IQueryable<Dog> query)
        {
            return (request.Order.ToLower() == "desc") ? query.OrderByDescending(x => x.Name)
                        : query = (request.Order.ToLower() == "asc") ? query.OrderBy(x => x.Name)
                        : throw new Exception("Invalid order specified.");
        }
    }
}
