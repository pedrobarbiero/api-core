using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Data.Context
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InactiveCascadeDelete(modelBuilder);
            SetVarchar100ToStringPropertiesWithoutMapping(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockContext).Assembly);
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly);
        }

        private static void SetVarchar100ToStringPropertiesWithoutMapping(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                            .SelectMany(e => e.GetProperties()
                                .Where(p => p.ClrType == typeof(string))))
            {
                property.Relational().ColumnType = "varchar(100)";
            }
        }

        private static void InactiveCascadeDelete(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
