using WebTutorial.Models.DTO;

namespace WebTutorial.Abstractions
{
    public interface IStoreRepository
    {
        public int AddCategory(CategoryDto category);
        public int DeleteCategory(CategoryDto category);

        public IEnumerable<CategoryDto> GetCategories();

        public int AddProduct(ProbuctDto product); 
        public int UpdatePriceProduct(ProbuctDto product);
        public int DeleteProduct(ProbuctDto product);

        public IEnumerable<ProbuctDto> GetProducts();
    }
}
