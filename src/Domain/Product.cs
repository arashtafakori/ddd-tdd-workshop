namespace Domain;

public class Product
{
    public string Id { get; private set; }
    public string? Name { get; private set; }

    public Product Define(string? name)
    {
        Name = name;
        
        if(string.IsNullOrEmpty(name))
            throw new NameCanNotBeNullOrEmptyException();

        if(name.Length < 3 || name.Length > 20)
            throw new ProductNameIsNotProperException(
                message: "The product name's length should be at least 3 characters and at most 20 characters.");

        Id = Guid.NewGuid().ToString();

        return this;
    }
}
