using Microsoft.EntityFrameworkCore;

namespace Persistence.MsSqlEFCore;

public class WarehouseDbContext : DbContext
{        
    public DbSet<ProductModel> Products { get; set; }
 
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
