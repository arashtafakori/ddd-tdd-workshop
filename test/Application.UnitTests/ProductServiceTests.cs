using Contract;
using Domain;
using Moq;

namespace Application.UnitTests;

public class ProductServiceTests
{
    [Fact]
    public async void Defining_Product_With_Unique_Name_Should_Succeed()
    {
        // Arrange
        var mockedRepository = new Mock<IProductRepository>();
        var productService = new ProductService(mockedRepository.Object);
        var command = new DefineProduct(name: "IPhone13");
        mockedRepository.Setup(r => r.Exists(command.Name)).ReturnsAsync(false);
        mockedRepository.SetupGet(r => r.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);
        // Act
        Func<Task> actual = async () => await productService.Define(command);

        // Act and Assert
        try
        {
            await actual.Invoke();
        }
        catch (Exception ex)
        {
            Assert.True(false, $"Unexpected exception: {ex.Message}");
        }
    }
    [Fact]
    public void Defining_Product_With_Duplicate_Name_Should_Fail()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var productService = new ProductService(mockRepository.Object);
        var command = new DefineProduct(name: "IPhone13");
        mockRepository.Setup(r => r.Exists(command.Name)).ReturnsAsync(true); 

        // Act
        Func<Task> actual = async () => await productService.Define(command);
        
        // Assert
        Assert.ThrowsAsync<ProductWithTheSameNameHasBeenAlreadyDefinedException>(actual);
    }
}
