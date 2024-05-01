using Application;
using Contract;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AcceptanceTests;

public class UserWantsToDefineAProduct : IClassFixture<ProductFixture>
{
    private IServiceScope _serviceScope;
    private readonly ProductFixture _fixture;
    public UserWantsToDefineAProduct(ProductFixture fixture)
    {
        _fixture = fixture;
        _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
    }
    /// <summary>
    /// As a user
    /// I want to define a product
    /// So that I should be able to access the product
    /// </summary>
    [Fact]
    public async Task GivenUserDefinesAProduct_WhenDefining_ThenShouldAccessTheProduct()
    {
        IProductService service = _serviceScope!.ServiceProvider.GetRequiredService<IProductService>();

        var productName = "Iphone15";

        // Given
        var command = new DefineProductCommand(productName);

        // When
        ProductViewModel? retrievedProduct = null;
        Func<Task> actual = async () => {
            var productId = await service.DefineProduct(command);
            retrievedProduct = await service.GetProduct(productId);
        };

        // Then
        try
        {
            await actual.Invoke();

            Assert.True(productName == retrievedProduct?.Name);
        }
        catch (Exception ex)
        {
            Assert.True(false, $"Unexpected exception: {ex.Message}");
        }

        // Tear down
        _fixture.EnsureRecreatedDatabase();
    }

    [Fact]
    public async Task GivenUserDefinesAProduct_AndGivenAProductWithThisNameHasBeenAlreadyDefined_WhenDefining_ThenShouldBePreventedFromDefiningTheNewAgain()
    {
        IProductService service = _serviceScope!.ServiceProvider.GetRequiredService<IProductService>();

        var productName = "Iphone15";

        // Given
        var command = new DefineProductCommand(productName);
        await service.DefineProduct(new DefineProductCommand(productName));

        // When
        Func<Task> actual = async () => await service.DefineProduct(command);

        // Then
        await Assert.ThrowsAsync<ProductWithTheSameNameHasBeenAlreadyDefinedException>(actual);

        // Tear down
        _fixture.EnsureRecreatedDatabase();
    }
}