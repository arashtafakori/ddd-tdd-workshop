namespace Domain;

public class ProductPriceCanNotBeNegativeException : Exception
{
    public ProductPriceCanNotBeNegativeException()
    : base(){ }
}
