using Backend.Data;
using Backend.Models.DTOs.Products;
using Backend.Models.Entity;
using Backend.Repositories.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _context.Products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,

        }).ToListAsync();
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        return productEntity == null ? null : new ProductDto
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Description = productEntity.Description,
            Price = productEntity.Price,
            ImageUrl = productEntity.ImageUrl,
        };
    }

    public async Task<ProductEntity> CreateAsync(ProductEntity product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product;

    }

    public async Task<ProductEntity> UpdateAsync(int id, ProductEntity product)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        
        if(existingProduct == null)
        {
            return null;
        }

        product.Id = id;
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.ImageUrl = product.ImageUrl;

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<ProductEntity> DeleteAsync(int id)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        
        if(existingProduct == null)
        {
            return null;
        }
        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync();

        return existingProduct;
    }
}
