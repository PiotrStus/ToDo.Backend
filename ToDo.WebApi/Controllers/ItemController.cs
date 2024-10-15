using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Logic.Item;

namespace ToDo.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ItemController : BaseController
    {

        public ItemController(ILogger<ItemController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateItemCommand.Request model)
        {
            var createItemResult = await _mediator.Send(model);

            return Ok(createItemResult);
        }
    }
}