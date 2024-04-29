namespace Application;

public class ProductWithTheSameNameHasBeenAlreadyDefinedException : Exception
{    public ProductWithTheSameNameHasBeenAlreadyDefinedException()
    : base("Product with the same name has been already defined."){ }
}
