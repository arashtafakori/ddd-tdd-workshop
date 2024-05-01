namespace Contract;

public class DefineProductCommand
{
    public string Name { get;private set; }

    public DefineProductCommand(string name)
    {
        Name = name;
    }
}