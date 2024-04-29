namespace Domain;

public interface IProductRepository
{
    public IUnitOfWork UnitOfWork { get; }

    public Task<bool> Exists(string productName);

    public Task<string> Define(Product product);
}
