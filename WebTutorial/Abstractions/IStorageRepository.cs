using WebTutorial.Models.DTO;

namespace WebTutorial.Abstractions
{
    public interface IStorageRepository
    {
        public int AddStorage(StorageDto storage);
        public int AddProductToStorage(int storageId, ProbuctDto product);
        public IEnumerable<ProbuctDto> GetStoragedProducts(string storageName);
    }
}
