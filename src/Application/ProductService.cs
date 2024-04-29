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
    public async Task<string> Define(DefineProduct command)
    {
        if(await _repository.Exists(command.Name))
            throw new ProductWithTheSameNameHasBeenAlreadyDefinedException();

        var product = new Product()
            .Define(name: command.Name);

        var projectId = await _repository.Define(product);
        await _repository.UnitOfWork.Commit();

        return projectId;
    }
}

