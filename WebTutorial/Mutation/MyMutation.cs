using WebTutorial.Abstractions;
using WebTutorial.Models.DTO;

namespace WebTutorial.Mutation
{
    public class MyMutation
    {
        public int AddProduct(ProbuctDto product, [Service] IStoreRepository repository)
        {
            return repository.AddProduct(product);
        }
    }
}
