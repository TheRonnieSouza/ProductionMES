using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductionMES.Application.Command.AddProduction;

namespace ProductionMES.Controllers
{
    public class ProductionController
    {
        private readonly IMediator _mediator;

        public ProductionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet("ValidateTraceabilityCode")]
        //public async Task<IActionResult> ValidateTraceabilityCode(ValidateTraceabilityQuery query)
        //{
        //    var productionOrders = await _mediator.Send(query);
        //    return Ok(productionOrders);
        //}

        [HttpPost("AddProduction")]
        public async Task<IActionResult> AddProduction(AddProductionCommand command)
        {
            var result = await _mediator.Send(command);
            return await NotFound();
        }

        private async Task<IActionResult> NotFound()
        {
            throw new NotImplementedException();
        }


        //[HttpPut]
        //public async Task<IActionResult> UpdatePartStatus(UpdatePartStatusCommand command)
        //{
        //    await _mediator.Send(command);
        //    return Ok();
        //}
    }
}
