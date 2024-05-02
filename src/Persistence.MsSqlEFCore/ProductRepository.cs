using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.MsSqlEFCore;

public class ProductRepository : IProductRepository
{
    private readonly WarehouseDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public ProductRepository(WarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
        _unitOfWork = new UnitOfWork(_dbContext);
    }

    public IUnitOfWork UnitOfWork { get => _unitOfWork; }

    public async Task<string> Define(Product product)
    {
        var productModel = new ProductModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = product.Name!,
            Price = product.Price.Value
        };
        _dbContext.Products.Add(productModel);
        return await Task.FromResult(productModel.Id);
    }

    public Task<bool> Exists(string productName)
    {
        return _dbContext.Products.Where(p => p.Name == productName).AnyAsync();
    }

    public async Task<Product> Get(string id)
    {
        var retrievedItem = await _dbContext.Products
            .Where(p => p.Id == id).FirstOrDefaultAsync();

        return Product.Instantiate(
            id: retrievedItem!.Id,
            name: retrievedItem.Name,
            new ProductPrice(retrievedItem.Price));
    }
}
