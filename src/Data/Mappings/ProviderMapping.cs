using System;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.HasOne(provider => provider.Address)
                .WithOne(adress => adress.Provider);            

            builder.HasMany(provider => provider.Products)
                .WithOne(product => product.Provider)
                .HasForeignKey(product => product.ProviderId);

            builder.ToTable("Providers", "Stock");
        }
    }
}
