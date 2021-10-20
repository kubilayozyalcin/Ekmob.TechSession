using Ekmob.TechSession.Application.Commands.CustomerCreate;
using Ekmob.TechSession.Application.Queries;
using Ekmob.TechSession.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ekmob.TechSession.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("GetCustomersByName/{Name}")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetOrdersByUserName(string name)
        {
            var query = new GetCustomersNameQuery(name);

            var orders = await _mediator.Send(query);
            if (orders.Count() == decimal.Zero)
                return NotFound();

            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerResponse>> OrderCreate([FromBody] CustomerCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
