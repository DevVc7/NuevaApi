using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.Password).HasColumnName("password");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Role).HasColumnName("role");
            builder.Property(e => e.CreateAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            
        }
    }
}
