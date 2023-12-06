using Microsoft.AspNetCore.Mvc;
using MSqlServerDbCRUD.Data;
using SqlServerDbCRUD.Model;

namespace SqlServerDbCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var hero = await _context.Products.FindAsync(id);
            if (hero == null)
                return BadRequest("Product not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Product>>> Update(Product request)
        {
            var dbProd = await _context.Products.FindAsync(request.Id);
            if (dbProd == null)
                return BadRequest("Product not found.");

            dbProd.Name = request.Name;
            dbProd.Description = request.Description;

            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> Delete(int id)
        {
            var dbProd = await _context.Products.FindAsync(id);
            if (dbProd == null)
                return BadRequest("Product not found.");

            _context.Products.Remove(dbProd);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

    }
}
