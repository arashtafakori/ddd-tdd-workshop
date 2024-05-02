namespace Contract;

public class DefineProductCommand
{
    public string Name { get;private set; }

    public decimal Price { get; private set; }

    public DefineProductCommand(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}