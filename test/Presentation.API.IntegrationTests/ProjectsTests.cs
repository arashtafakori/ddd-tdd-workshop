using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Contract;
using Newtonsoft.Json;

namespace Presentation.API.IntegrationTests
{
    public class ProductsTests : IClassFixture<ProductsFixture>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductsTests(ProductsFixture fixture)
        {
            var serviceScope = fixture.ServiceProvider.CreateAsyncScope();
            _httpClientFactory = serviceScope.ServiceProvider
                .GetRequiredService<IHttpClientFactory>();
        }
 
        [Fact]
        public async Task GetProduct_ReturnsSuccessStatusCode()
        {
            // Arrange
            var productName = "Iphone A";
            var request = new DefineProductCommand(name: productName, price: 1000);
            string jsonProduct = JsonConvert.SerializeObject(request);

            // Act
            ProductViewModel? retrievedProduct = null;
            HttpResponseMessage? responseOfGet = null;
            using (HttpClient client = _httpClientFactory.CreateClient(HttpClientNames.WebAPIClient))
            {
                StringContent content = new StringContent(jsonProduct, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseOfDefine = await client.PostAsync("/products", content);

                if (responseOfDefine.IsSuccessStatusCode)
                {
                    var productId = JsonConvert.DeserializeObject<string>(
                        await responseOfDefine.Content.ReadAsStringAsync());

                    responseOfGet = await client.GetAsync($"/products/{productId}");

                    if (responseOfGet!.IsSuccessStatusCode)
                    {
                        retrievedProduct = JsonConvert.DeserializeObject<ProductViewModel>(
                            await responseOfGet.Content.ReadAsStringAsync());
                    }
                }
            }

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseOfGet?.StatusCode);
            Assert.True(productName == retrievedProduct?.Name);
        }

        [Fact]
        public async Task DefineProduct_ReturnsSuccessStatusCode()
        {
            // Arrange
            var productName = "Iphone B";
            var request = new DefineProductCommand(name: productName, price: 1000);
            string jsonProduct = JsonConvert.SerializeObject(request);

            // Act
            ProductViewModel? retrievedProduct = null;
            HttpResponseMessage? responseOfDefine = null;
            using (HttpClient client = _httpClientFactory.CreateClient(HttpClientNames.WebAPIClient))
            {
                StringContent content = new StringContent(jsonProduct, System.Text.Encoding.UTF8, "application/json");
                responseOfDefine = await client.PostAsync("/products", content);

                if (responseOfDefine.IsSuccessStatusCode)
                {
                    var productId = JsonConvert.DeserializeObject<string>(
                        await responseOfDefine.Content.ReadAsStringAsync());

                    var responseOfGet = await client.GetAsync($"/products/{productId}");
                    if (responseOfGet!.IsSuccessStatusCode)
                    {
                        retrievedProduct = JsonConvert.DeserializeObject<ProductViewModel>(
                            await responseOfGet.Content.ReadAsStringAsync());
                    }
                }
            }

            // Assert
            Assert.Equal(HttpStatusCode.Created, responseOfDefine?.StatusCode);
            Assert.True(productName == retrievedProduct?.Name);
        }

    }
}
