using Microsoft.AspNetCore.Mvc;
using WebTutorial.Abstractions;
using WebTutorial.Models;
using WebTutorial.Models.DTO;

namespace WebTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly IStoreRepository _storeRepository;

        public CategoryController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpPost("add_category")]
        public IActionResult AddCategory([FromBody] CategoryDto category)
        {
            var result = _storeRepository.AddCategory(category);
            return Ok(result);
        }

        [HttpDelete("del_category")]
        public IActionResult DeleteCategory([FromBody] CategoryDto category)
        {
            var result = _storeRepository.DeleteCategory(category);
            return Ok(result);
        }

        [HttpGet("get_categories")]
        public IActionResult GetCategories()
        {
            var categories = _storeRepository.GetCategories();
            return Ok(categories);
        }
    }
}
