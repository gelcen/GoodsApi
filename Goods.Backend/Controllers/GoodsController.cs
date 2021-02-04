using Goods.Backend.Data;
using Goods.Backend.Models;
using Goods.Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goods.Backend.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class GoodsController : Controller
    {
        private readonly GoodContext _context;

        public GoodsController(GoodContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Good>>> GetGoods()
		{
            return await _context.Goods.ToListAsync();
		}

        [HttpGet("{id}")]
        public async Task<ActionResult> GetGood(int id)
        {
            var good = await _context.Goods.FindAsync(id);

            if (good == null)
            {
                return NotFound();
            }

            return Ok(good);
        }


        [HttpPost]
        public async Task<IActionResult> CreateGood(
            [FromBody] CreateGoodViewModel goodVm)
        {
            if (ModelState.IsValid)
            {
                _context.Goods.Add(new Good 
                { 
                    Name = goodVm.Name,
                    Price = goodVm.Price
                });
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGood(int id, Good good)
		{
            if (id != good.Id)
			{
                return BadRequest();
			}

            _context.Entry(good).State = EntityState.Modified;

			try
			{
                await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
                if (!GoodExists(id))
				{
                    return NotFound();
				}
                else
				{
				    throw;
				}
			}

            return NoContent();
		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGood(int id)
		{
            var good = await _context.Goods.FindAsync(id);
            if (good == null)
			{
                return NotFound();
			}
            _context.Goods.Remove(good);
            await _context.SaveChangesAsync();
            return Ok();
		}

        private bool GoodExists(int id)
        {
            return _context.Goods.Any(x => x.Id == id);
        }
    }
}
