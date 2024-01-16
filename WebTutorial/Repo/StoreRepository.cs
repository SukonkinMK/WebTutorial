using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using WebTutorial.Abstractions;
using WebTutorial.Models;
using WebTutorial.Models.DTO;

namespace WebTutorial.Repo
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private StoreContext _context;

        public StoreRepository(IMapper mapper, IMemoryCache cache, StoreContext context)
        {
            _mapper = mapper;
            _cache = cache;
            _context = context;
        }

        public int AddCategory(CategoryDto category)
        {
            using(_context)
            {
                var entity = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(category.Name.ToLower()));
                if (entity == null)
                {
                    entity = _mapper.Map<Category>(category);
                    _context.Categories.Add(entity);
                    _context.SaveChanges();
                    _cache.Remove("groups");
                }
                return entity.Id;
            }
        }

        public int DeleteCategory(CategoryDto category)
        {
            using (_context)
            {
                var entity = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(category.Name.ToLower()));
                if (entity != null)
                {
                    _context.Categories.Remove(entity);
                    _context.SaveChanges();
                    _cache.Remove("groups");
                    return 1;
                }
                return 0;
            }            
        }

        public int AddProduct(ProbuctDto product)
        {
            using (_context)
            {
                var entity = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(product.Name.ToLower()));
                if (entity == null)
                {
                    entity = _mapper.Map<Product>(product);
                    _context.Products.Add(entity);
                    _context.SaveChanges();
                    _cache.Remove("products");
                }
                return entity.Id;
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            if(_cache.TryGetValue("groups", out List<CategoryDto> categories))
            {
                return categories;
            }
            using (_context)
            {
                var groupsList = _context.Categories.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
                _cache.Set("groups", groupsList, TimeSpan.FromMinutes(30));
                return groupsList;
            }
        }

        public IEnumerable<ProbuctDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProbuctDto> products))
            {
                return products;
            }
            using (_context)
            {
                List<ProbuctDto> productList = _context.Products.Select(x => _mapper.Map<ProbuctDto>(x)).ToList();
                _cache.Set("products", productList, TimeSpan.FromMinutes(30));
                return productList;
            }
        }

        public int DeleteProduct(ProbuctDto product)
        {
            using (_context)
            {
                var entity = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(product.Name.ToLower()));
                if (entity != null)
                {
                    _context.Products.Remove(entity);
                    _context.SaveChanges();
                    _cache.Remove("products");
                    return 1;
                }
                return 0;
            }
        }

        public int UpdatePriceProduct(ProbuctDto product)
        {
            using (_context)
            {
                var entity = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(product.Name.ToLower()));
                if (entity != null)
                {
                    entity.Price = product.Price;
                    _context.SaveChanges();
                    _cache.Remove("products");
                    return 1;
                }
                return 0;
            }
        }
    }
}
