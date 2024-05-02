using Contract;
using Domain;

namespace Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> DefineProduct(DefineProductCommand command)
    {
        if(await _repository.Exists(command.Name))
            throw new ProductWithTheSameNameHasBeenAlreadyDefinedException();

        var product = new Product()
            .Define(name: command.Name, new ProductPrice(command.Price));

        var projectId = await _repository.Define(product);
        await _repository.UnitOfWork.Commit();

        return projectId;
    }

    public async Task<ProductViewModel> GetProduct(string productId)
    {
        var product = await _repository.Get(productId);

        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name!,
            Price = product.Price.Value
        };
    }
}

