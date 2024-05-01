namespace AcceptanceTests
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