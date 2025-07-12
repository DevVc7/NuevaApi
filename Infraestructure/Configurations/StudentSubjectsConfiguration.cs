using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class StudentSubjectsConfiguration : IEntityTypeConfiguration<StudentSubjects>
    {
        public void Configure(EntityTypeBuilder<StudentSubjects> builder)
        {
            builder.ToTable("student_subjects");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Progress).HasColumnName("progress");
            builder.Property(e => e.SubjectId).HasColumnName("subject_id");
            builder.Property(e => e.StudentId).HasColumnName("student_id");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");


            builder.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId);
            builder.HasOne(e => e.Student).WithMany(s => s.StudentSubjects).HasForeignKey(e => e.StudentId);
        }
    }
}
