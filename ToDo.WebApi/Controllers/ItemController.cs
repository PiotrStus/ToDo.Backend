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
        public async Task<ActionResult> CreateItem([FromBody] CreateItemCommand.Request model)
        {
            var createItemResult = await _mediator.Send(model);

            return Ok(createItemResult);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateItem([FromBody] UpdateItemCommand.Request model)
        {
            var updateItemResult = await _mediator.Send(model);

            return Ok(updateItemResult);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteItem([FromBody] DeleteItemCommand.Request model)
        {
            var deleteItemResult = await _mediator.Send(model);

            return Ok(deleteItemResult);
        }

        [HttpPost]
        public async Task<ActionResult> MarkItemAsCompleted([FromBody] MarkItemAsCompletedCommand.Request model)
        {
            var markItemResult = await _mediator.Send(model);

            return Ok(markItemResult);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllItems([FromQuery] GetAllItemsQuery.Request model)
        {
            var data = await _mediator.Send(model);
            return Ok(data);
        }
    }
}