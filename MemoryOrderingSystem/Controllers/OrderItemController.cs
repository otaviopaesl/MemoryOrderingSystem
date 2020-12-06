using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoryOrderingSystem.Models;
using MemoryOrderingSystem.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace MemoryOrderingSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemsController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Sucesso!", typeof(IEnumerable<OrderItem>))]
        public async Task<IActionResult> GetOrderItem()
        {
            return Ok(await _orderItemService.Get());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(OrderItem))]
        [SwaggerResponse(404, "Não encontrado!")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            var orderItem = await _orderItemService.Get(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(IEnumerable<OrderItem>))]
        [SwaggerResponse(404, "Não encontrado!")]
        [SwaggerResponse(400, "Erro!", typeof(Dictionary<string, IEnumerable<string>>))]
        public async Task<IActionResult> PutOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            if (id != orderItem.Id || id == 0 || orderItem.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                await _orderItemService.Update(orderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _orderItemService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost]
        [SwaggerResponse(201, "Sucesso!", typeof(OrderItem))]
        public async Task<ActionResult> PostOrderItem(OrderItem orderItem)
        {
            await _orderItemService.Insert(orderItem);
            return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(OrderItem))]
        [SwaggerResponse(404, "Não encontrado!")]
        public async Task<ActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _orderItemService.Get(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            await _orderItemService.Remove(orderItem);

            return Ok(orderItem);
        }
    }
}
