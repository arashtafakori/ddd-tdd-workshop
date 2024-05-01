namespace Presentation.API.IntegrationTests
{
    public class ProductsFixture : ServiceContext, IDisposable
    {
        public ProductsFixture()
        {
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}