using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configurations
{
    internal class GymUserConfigurations<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : GymUser
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(u => u.Name).HasColumnType("varchar").HasMaxLength(70);
            builder.Property(u => u.Email).HasColumnType("varchar").HasMaxLength(240);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'");
                tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%'");

            });

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();


            builder.OwnsOne(u => u.Address, addressbuilder =>
            {
                addressbuilder.Property(a => a.Street)
                              .HasColumnName("Street")
                              .HasMaxLength(120);
                addressbuilder.Property(a => a.City)
                              .HasColumnName("City")
                              .HasMaxLength(120);               
            });
        }
    }
}
