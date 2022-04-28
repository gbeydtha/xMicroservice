using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductByName(string category);
        Task<IEnumerable<Product>> GetProductByCatagory(string category);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task CreateProduct(Product product);
    }
}
