using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        public CourseMap()
        {
            HasKey(course => course.CourseId);

            Property(course => course.CourseId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(course => course.CourseNumber).IsRequired();

            ToTable("Courses");

            HasMany(entity => entity.Specialties);
        }
    }
}
