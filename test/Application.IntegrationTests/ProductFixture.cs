namespace Application.IntegrationTests
{
    public class ProductFixture : ServiceContext, IDisposable
    {
        public ProductFixture()
        {
        }

        void IDisposable.Dispose()
        {
            EnsureRecreatedDatabase();
            Dispose();
        }
    }
}