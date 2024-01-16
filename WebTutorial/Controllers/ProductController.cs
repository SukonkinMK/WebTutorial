using Microsoft.AspNetCore.Mvc;
using WebTutorial.Abstractions;
using WebTutorial.Models;
using WebTutorial.Models.DTO;

namespace WebTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IStoreRepository _storeRepository;

        public ProductController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet("get_products")]
        public IActionResult GetProducts()
        {
            var products = _storeRepository.GetProducts();
            return Ok(products);
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromBody] ProbuctDto productDto)
        {
            var result = _storeRepository.AddProduct(productDto);
            return Ok(result);
        }

        [HttpDelete("del_product")]
        public IActionResult DeleteProduct([FromBody] ProbuctDto productDto)
        {
            var result = _storeRepository.DeleteProduct(productDto);
            return Ok(result);
        }

        [HttpPatch("addProductPrice")]
        public IActionResult AddProductPrice([FromBody] ProbuctDto productDto)
        {
            var result = _storeRepository.UpdatePriceProduct(productDto);
            return Ok(result);
        }
    }
}
