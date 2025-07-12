using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(e => e.CreateAt)
            .HasColumnName("createAt")
            .HasDefaultValueSql("SYSDATETIME()");

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updatedAt")
                .HasDefaultValueSql("SYSDATETIME()");
        }
    }
}