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
            var command = new DefineProduct(name: "IPhone16");

            // Act
            Func<Task> actual = async () => await service.Define(command);

            // Assert
            try
            {
                await actual.Invoke();
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