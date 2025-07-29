using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/controller")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private IProductService _iProductService { get; set; }
        public ProductsController(ILogger<ProductsController> logger, IProductService iProductService)
        {
            _logger = logger;
            _iProductService = iProductService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.Product>>> Get()
        {
            _logger.Log(LogLevel.Information, "Getting Product");

            return Ok(await _iProductService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Product>> Get(int id)
        {
            _logger.Log(LogLevel.Information, $"Getting product {id}");

            var product = await _iProductService.GetByIdAsync(id);

            if (product == null) 
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Model.Product>> Post(Product product)
        {
            _logger.Log(LogLevel.Information, $"Posting product");

            var newProduct = await _iProductService.CreateAsync(product);

            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Model.Product product)
        {
            _logger.Log(LogLevel.Information, $"Putting product {id}");

            var result = await _iProductService.UpdateAsync(id, product);

            if (!result) 
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Log(LogLevel.Information, $"Deleting product");

            var result = await _iProductService.DeleteAsync(id);

            if (!result) 
                return NotFound();

            return NoContent();
        }
    }
}