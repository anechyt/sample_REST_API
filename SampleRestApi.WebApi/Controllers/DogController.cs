using Mediator;
using Microsoft.AspNetCore.Mvc;
using SampleRestApi.Persistence;

namespace SampleRestApi.WebApi.Controllers
{
    [Route("api/[controller]")]
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
    }
}
