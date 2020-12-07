using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemoryOrderingSystem.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using MemoryOrderingSystem.Services.Interfaces;
using System;

namespace MemoryOrderingSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("[action]/{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(Order))]
        [SwaggerResponse(404, "Não encontrado!")]
        public async Task<IActionResult> SearchOrder(int id)
        {
            var order = await _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("[action]/{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(Order))]
        [SwaggerResponse(400, "Erro!", typeof(Dictionary<string, IEnumerable<string>>))]
        public async Task<IActionResult> UpdateOrder(int id, Models.Enums.OrderStatus orderStatus)
        {
            try
            {
                var order = await _orderService.UpdateStatus(id, orderStatus);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]/{sellerId}")]
        [SwaggerResponse(201, "Sucesso!", typeof(Order))]
        [SwaggerResponse(400, "Erro!", typeof(Dictionary<string, IEnumerable<string>>))]
        public async Task<ActionResult> RegisterOrder(int sellerId, List<OrderItem> orderItems)
        {
            if (orderItems.Count < 1)
            {
                return BadRequest("O pedido deve possuir pelo menos um item.");
            }

            try
            {
                var result = await _orderService.Insert(sellerId, orderItems);
                return CreatedAtAction("SearchOrder", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
