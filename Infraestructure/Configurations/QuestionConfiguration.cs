using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("questions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.SubjectId).HasColumnName("subject_id");
            builder.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
            builder.Property(e => e.Type).HasColumnName("type").HasMaxLength(50);
            builder.Property(e => e.Content).HasColumnName("content").IsRequired();
            builder.Property(e => e.Options).HasColumnName("options");
            builder.Property(e => e.CorrectAnswer).HasColumnName("correct_answer");
            builder.Property(e => e.Points).HasColumnName("points").HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(e => e.Difficulty).HasColumnName("difficulty");
            builder.Property(e => e.CreatedBy).HasColumnName("created_by");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").IsRequired();
            
            
            builder.HasOne(e => e.Subjects).WithMany().HasForeignKey(e => e.SubjectId);
        }
    }
}
