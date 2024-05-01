namespace Contract;

public interface IProductService
{
    public Task<string> DefineProduct(DefineProductCommand command);
    public Task<ProductViewModel> GetProduct(string productId);
}
