using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("student");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Grade).HasColumnName("grade");
            builder.Property(e => e.UsersId).HasColumnName("users_id");
            builder.Property(e => e.Progress).HasColumnName("progress");
            builder.Property(e => e.LastActive).HasColumnName("lastActive");
            builder.Property(e => e.Code).HasColumnName("code");
            builder.Property(e => e.Note).HasColumnName("note");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UsersId);
        }
    }
}