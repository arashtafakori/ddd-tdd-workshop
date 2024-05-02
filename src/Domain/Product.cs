using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Domain;

public class Product
{
    public string Id { get; private set; }
    public string? Name { get; private set; }
    public ProductPrice Price{ get; private set; }

    public Product Define(string? name, ProductPrice price)
    {
        Name = name;
        Price = price;

        if (string.IsNullOrEmpty(name))
            throw new NameCanNotBeNullOrEmptyException();

        if(name.Length < 3 || name.Length > 20)
            throw new ProductNameIsNotProperException(
                message: "The product name's length should be at least 3 characters and at most 20 characters.");

        Price.Validate();

        Id = Guid.NewGuid().ToString();

        return this;
    }

    public static Product Instantiate(string id, string? name, ProductPrice price)
    {
        return new Product()
        {
            Id = id,
            Name = name,
            Price = price
        };
    }
}
