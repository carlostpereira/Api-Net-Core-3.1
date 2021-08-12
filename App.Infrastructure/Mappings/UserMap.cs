using System;
using App.Domain.Entities;
using App.Shared.Enums;
using App.SharedKernel.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {

            //Entity
            entity.ToTable("your_project_user");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Name).IsRequired().HasMaxLength(50).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Password).IsRequired().HasMaxLength(128).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(160).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Active).IsRequired().HasColumnType(Constants.Boolean);
            entity.Property(x => x.Gender).IsRequired().IsFixedLength().HasMaxLength(10).IsUnicode(false).HasColumnType(Constants.Varchar)
                .HasConversion(x => x.ToString(), x => Enum.Parse<Gender>(x));
            entity.Property(x => x.Role).IsRequired().IsFixedLength().HasMaxLength(10).IsUnicode(false).HasColumnType(Constants.Varchar)
                .HasConversion(x => x.ToString(), x => Enum.Parse<Role>(x));

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality


        }
    }
}
