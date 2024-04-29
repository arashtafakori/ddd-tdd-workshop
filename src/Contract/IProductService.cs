namespace Contract;

public interface IProductService
{
    public Task<string> Define(DefineProduct command);
}
