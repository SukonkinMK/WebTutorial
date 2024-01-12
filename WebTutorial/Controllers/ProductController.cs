using Microsoft.AspNetCore.Mvc;
using WebTutorial.Models;

namespace WebTutorial.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        [HttpGet("getProduct")]
        public IActionResult GetProducts()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    var products = context.Products.Select(x => new Product()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    });
                    return Ok(products.ToList());
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("addProduct")]
        public IActionResult AddProduct([FromQuery] string name, string description, string categoryName, int price)
        {
            try
            {
                using (var context = new StoreContext())
                {
                    Category category = null;
                    if(!context.Categories.Any(x => x.Name.ToLower().Equals(name.ToLower()))) {
                        category = new Category() { Name = categoryName };
                        context.Categories.Add(category);
                        context.SaveChanges();
                    }
                    else
                    {
                        category = context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
                    }

                    if (!context.Products.Any(x => x.Name.ToLower().Equals(name.ToLower())) && category != null)
                    {
                        var product = new Product() { Name = name, Description = description, Category = category, CategoryId = category.Id, Price = price};
                        context.Add(product);
                        context.SaveChanges();
                        return Ok(product.Id);
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
