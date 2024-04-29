namespace Domain;

public class NameCanNotBeNullOrEmptyException : Exception
{
    public NameCanNotBeNullOrEmptyException()
    : base("Name can not be null or empty."){ }
}