using App.Domain.Entities;
using App.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Mappings
{
    public class ProviderMap : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> entity)
        {

            //Entity
            entity.ToTable("your_project_provider");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(160).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Cnpj).IsRequired().HasMaxLength(14).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Active).IsRequired().HasColumnType(Constants.Boolean);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality


        }
    }
}
