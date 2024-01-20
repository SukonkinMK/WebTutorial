using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebTutorial.Models.DTO;
using WebTutorial.Models;
using WebTutorial.Abstractions;

namespace WebTutorial.Repo
{
    public class StorageRepository : IStorageRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private StoreContext _context;

        public StorageRepository(IMapper mapper, IMemoryCache cache, StoreContext context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }              

        public int AddProductToStorage(int storageId, ProbuctDto product)
        {
            using (_context)
            {
                var entityProduct = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(product.Name.ToLower()));
                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Product>(product);
                    _context.Products.Add(entityProduct);
                    _context.SaveChanges();
                }
                
                var entityStorage = _context.Storages.FirstOrDefault(x => x.Id.Equals(storageId));
                if(entityStorage != null) 
                {
                    entityStorage.Products.Add(entityProduct);
                    entityStorage.Count = entityStorage.Products.Count();
                    _context.SaveChanges();
                    return entityStorage.Count;
                }
                return -1;
            }
        }

        public int AddStorage(StorageDto storage)
        {
            using (_context)
            {
                var entity = _context.Storages.FirstOrDefault(x => x.Name.ToLower().Equals(storage.Name.ToLower()));
                if (entity == null)
                {
                    entity = _mapper.Map<Storage>(storage);
                    _context.Storages.Add(entity);
                    _context.SaveChanges();
                }
                return entity.Id;
            }
        }

        public IEnumerable<ProbuctDto> GetStoragedProducts(string storageName)
        {            
            using (_context)
            {
                var entityStorage = _context.Storages.FirstOrDefault(x => x.Name.ToLower().Equals(storageName));
                if (entityStorage != null)
                {
                    List<ProbuctDto> productList = entityStorage.Products.Select(x => _mapper.Map<ProbuctDto>(x)).ToList();
                    return productList;
                }
                return Enumerable.Empty<ProbuctDto>();
            }
        }
    }
}
