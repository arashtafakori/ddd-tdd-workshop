namespace Domain;

public class ProductNameIsNotProperException : Exception
{
    public ProductNameIsNotProperException(string? message)
    : base(message){ }
}
