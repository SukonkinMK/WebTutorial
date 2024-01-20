using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using WebTutorial.Abstractions;
using WebTutorial.Models.DTO;

namespace WebTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : Controller
    {
        private readonly IStorageRepository _storageRepository;

        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        [HttpPost("add_storage")]
        public IActionResult AddStorage([FromBody] StorageDto storage)
        {
            var result = _storageRepository.AddStorage(storage);
            return Ok(result);
        }

        [HttpPost("add_product_to_storage")]
        public IActionResult AddProductToStorage([FromBody] ProbuctDto product, [FromQuery] int storageId)
        {
            var result = _storageRepository.AddProductToStorage(storageId, product);
            return Ok(result);
        }

        [HttpGet("get_storage_products")]
        public IActionResult GetStoragedProducts([FromQuery] string storageName)
        {
            var products = _storageRepository.GetStoragedProducts(storageName);
            return Ok(products);
        }
    }
}
