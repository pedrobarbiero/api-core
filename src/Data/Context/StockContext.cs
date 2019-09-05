using System;
using Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
