using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;
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

        [HttpGet("export_products_csv")]
        public IActionResult ExportProductsCsv()
        {
            var products = _storeRepository.GetProducts();
            var content = getCSVString(products);
            string filename = "productReport" + DateTime.Now.ToBinary().ToString()+ ".csv";
            System.IO.File.WriteAllText(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", filename), content);
            return Ok("https://"+Request.Host.ToString() + "/static/"+ filename);
        }

        private string getCSVString(IEnumerable<ProbuctDto> products)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var product in products)
            {
                sb.AppendLine(product.Name + ";" + product.Price);
            }
            return sb.ToString();
        }       
    }
}
