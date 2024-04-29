using Domain;

namespace Persistence.MsSqlEFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WarehouseDbContext _context;

        public UnitOfWork(WarehouseDbContext context) =>
            _context = context;

        public async Task<bool> Commit()
        {
            var success = (await _context.SaveChangesAsync()) > 0;

            return success;
        }

        public void Dispose() =>
            _context.Dispose();

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
