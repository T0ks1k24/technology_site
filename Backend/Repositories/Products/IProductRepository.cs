
using Backend.Models.DTOs.Products;
using Backend.Models.Entity;

namespace Backend.Repositories.Product
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductEntity> CreateAsync (ProductEntity product);
        Task<ProductEntity> UpdateAsync (int id, ProductEntity product);
        Task<ProductEntity> DeleteAsync (int id);
    }
}
