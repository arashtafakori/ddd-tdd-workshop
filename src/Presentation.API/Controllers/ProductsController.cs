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

        [HttpPost]
        public async Task<ActionResult<string>> Define(DefineProduct command)
        {
            var id = await _productService.Define(command);
            return StatusCode(201, id);
        }
    }
}