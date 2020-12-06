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
    public class SellersController : ControllerBase
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        [SwaggerResponse(200, "Sucesso!", typeof(IEnumerable<Seller>))]
        public async Task<IActionResult> GetSeller()
        {
            return Ok(await _sellerService.Get());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(Seller))]
        [SwaggerResponse(404, "Não encontrado!")]
        public async Task<IActionResult> GetSeller(int id)
        {
            var seller = await _sellerService.Get(id);

            if (seller == null)
            {
                return NotFound();
            }

            return Ok(seller);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(IEnumerable<Seller>))]
        [SwaggerResponse(404, "Não encontrado!")]
        [SwaggerResponse(400, "Erro!", typeof(Dictionary<string, IEnumerable<string>>))]
        public async Task<IActionResult> PutSeller(int id, [FromBody] Seller seller)
        {
            if (id != seller.Id || id == 0 || seller.Id == 0)
            {
                return BadRequest();
            }

            try
            {
                await _sellerService.Update(seller);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _sellerService.Exists(id))
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
        [SwaggerResponse(201, "Sucesso!", typeof(Seller))]
        public async Task<ActionResult> PostSeller(Seller seller)
        {
            await _sellerService.Insert(seller);
            return CreatedAtAction("GetSeller", new { id = seller.Id }, seller);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Sucesso!", typeof(Seller))]
        [SwaggerResponse(404, "Não encontrado!")]
        public async Task<ActionResult> DeleteSeller(int id)
        {
            var seller = await _sellerService.Get(id);

            if (seller == null)
            {
                return NotFound();
            }

            await _sellerService.Remove(seller);

            return Ok(seller);
        }
    }
}
