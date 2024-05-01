using Contract;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(string id)
        {
            return await _productService.GetProduct(id);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Define(DefineProductCommand command)
        {
            var id = await _productService.DefineProduct(command);
            return StatusCode(201, id);
        }
    }
}