using Mediator;
using Microsoft.AspNetCore.Mvc;
using SampleRestApi.Application.Dogs.Commands.CreateDog;
using SampleRestApi.Application.Dogs.Queries.GetAllDogs;
using SampleRestApi.Persistence;

namespace SampleRestApi.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SampleRestApiContext _context;

        public DogController(IMediator mediator, SampleRestApiContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }

        [HttpPost("dog")]
        public async Task<IActionResult> CreateDog([FromBody] CreateDogRequest createDogRequest)
        {
            var result = await _mediator.Send(createDogRequest);
            return Ok(result);
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetAllDogs([FromQuery] GetAllDogsRequest getAllDogsRequest)
        {
            var result = await _mediator.Send(getAllDogsRequest);
            return Ok(result);
        }
    }
}
