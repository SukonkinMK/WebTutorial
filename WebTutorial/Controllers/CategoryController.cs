using Microsoft.AspNetCore.Mvc;
using WebTutorial.Models;

namespace WebTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        [HttpPost("addCategory")]
        public IActionResult AddCategory([FromQuery] string categoryName, string catgoryDescription)
        {
            try
            {
                using (var context = new StoreContext())
                {

                    if (!context.Categories.Any(x => x.Name.ToLower().Equals(categoryName.ToLower())))
                    {
                        Category category = new Category() { Name = categoryName, Description = catgoryDescription };
                        context.Categories.Add(category);
                        context.SaveChanges();
                        return Ok(category.Id);
                    }
                    else
                        return StatusCode(409);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("delCategory")]
        public IActionResult DeleteCategory([FromQuery] string name)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var category = context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
                    if (category != null)
                    {
                        context.Categories.Remove(category);
                        context.SaveChanges();
                        return Ok(category.Id);
                    }
                    else
                        return StatusCode(409);
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
