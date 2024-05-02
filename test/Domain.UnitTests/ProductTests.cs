namespace Domain.UnitTests;

public class ProductTests
{
    [Fact]
    public void Define_ShouldSucceed_WithTheProperValues()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Define(name: "IPhone13", price: new ProductPrice(1000));

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
        Action actual = () => product.Define(name: productName, price: new ProductPrice(1000));

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
        Action actual = () => product.Define(name: productName, price: new ProductPrice(1000));

        // Assert
        Assert.Throws<ProductNameIsNotProperException>(actual);
    }

    [Fact]
    public void Define_ShouldFail_IfProductPriceIsNegative()
    {
        // Arrange
        var product = new Product();

        // Act
        Action actual = () => product.Define(name: "IPhone13", price: new ProductPrice(-100));

        // Assert
        Assert.Throws<ProductPriceCanNotBeNegativeException>(actual);
    }
}

