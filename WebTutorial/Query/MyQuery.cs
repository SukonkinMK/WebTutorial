using WebTutorial.Abstractions;
using WebTutorial.Models.DTO;

namespace WebTutorial.Query
{
    public class MyQuery
    {
        public IEnumerable<ProbuctDto> GetProducts([Service] IStoreRepository repository) => repository.GetProducts();
        public IEnumerable<CategoryDto> GetCategories([Service] IStoreRepository repository) => repository.GetCategories();
    }
}
