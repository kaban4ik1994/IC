using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class SpecialtyMap : EntityTypeConfiguration<Specialty>
    {
        public SpecialtyMap()
        {
            HasKey(specialty => specialty.SpecialtyId);

            Property(specialty => specialty.SpecialtyId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(specialty => specialty.Name).IsRequired();

            ToTable("Specialties");

            HasMany(entity => entity.Groups);
            HasRequired(entity => entity.Course)
                .WithMany(course => course.Specialties)
                .HasForeignKey(specialty => specialty.CourseId);
        }
    }
}
