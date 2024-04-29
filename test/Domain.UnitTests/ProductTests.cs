namespace Domain.UnitTests;

public class ProductTests
{
    [Fact]
    public void Define_ShouldSucceed_WithTheProperValues()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Define(name: "IPhone13");

        // Assert
        Assert.True(true);
    }
    
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Define_ShouldFail_IfProductNameIsNullOrEmpty(string? productName)
    {
        // Arrange
        var product = new Product();

        // Act
        Action actual = () => product.Define(name: productName);

        // Assert
        Assert.Throws<NameCanNotBeNullOrEmptyException>(actual);
    }

    [Theory]
    [InlineData("s")]
    [InlineData("Sumsung galaxy erta 123 made in korea - yellow color")]
    public void Define_ShouldFail_IfProductNameIsNotProper(string? productName)
    {
        // Arrange
        var product = new Product();
        
        // Act
        Action actual = () => product.Define(name: productName);

        // Assert
        Assert.Throws<ProductNameIsNotProperException>(actual);
    }
}

