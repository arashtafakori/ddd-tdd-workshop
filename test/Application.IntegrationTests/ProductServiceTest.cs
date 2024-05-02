using Azure.Core;
using Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests
{
    public class ProductServiceTest : IClassFixture<ProductFixture>
    {
        private IServiceScope _serviceScope;
        private readonly ProductFixture _fixture;
        public ProductServiceTest(ProductFixture fixture)
        {
            _fixture = fixture;
            _serviceScope = _fixture.ServiceProvider.CreateAsyncScope();
        }

        [Fact]
        public async Task Define_Product_Successfully()
        {
            IProductService service = _serviceScope!.ServiceProvider.GetRequiredService<IProductService>();

            // Arrange
            var productName = "IPhone15";
            var command = new DefineProductCommand(name: productName, price: 1000);

            // Act
            string? productId = null;
            Func<Task> actual = async () =>
            {
                productId = await service.DefineProduct(command);
            };

            // Assert
            try
            {
                await actual.Invoke();

                var retrievedProduct = await service.GetProduct(productId!);

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
        public async Task Get_Product_Successfully()
        {
            IProductService service = _serviceScope!.ServiceProvider.GetRequiredService<IProductService>();

            // Arrange
            var productName = "IPhone15";
            var command = new DefineProductCommand(name: productName, price: 1000);
            var productId = await service.DefineProduct(command);

            // Act

            ProductViewModel? retrievedProduct = null;
            Func<Task> actual = async () => {
                retrievedProduct = await service.GetProduct(productId);
            };

            // Assert
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
    }
}