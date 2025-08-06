using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;

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

            var products = await _iProductService.GetAllAsync();

            _logger.Log(LogLevel.Information, "Product got it");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Model.Product>> Get(int id)
        {
            _logger.Log(LogLevel.Information, $"Getting product {id}");

            var product = await _iProductService.GetByIdAsync(id);

            _logger.Log(LogLevel.Information, $"Product {id} got it");

            if (product == null) 
                return NotFound();

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Model.Product>> Post(Model.Product product)
        {
            _logger.Log(LogLevel.Information, $"Posting product");

            var newProduct = await _iProductService.CreateAsync(product);

            _logger.Log(LogLevel.Information, $"Product {newProduct.Id} posted");

            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Model.Product product)
        {
            _logger.Log(LogLevel.Information, $"Putting product {id}");

            var result = await _iProductService.UpdateAsync(id, product);

            if (!result)
            {
                _logger.Log(LogLevel.Information, $"Product {id} was not found");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, $"Product put");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Log(LogLevel.Information, $"Deleting product {id}");

            var result = await _iProductService.DeleteAsync(id);

            if (!result)
            {
                _logger.Log(LogLevel.Information, $"Product {id} was not found");
                return NotFound();
            }

            _logger.Log(LogLevel.Information, $"Product deleted");

            return NoContent();
        }
    }
}