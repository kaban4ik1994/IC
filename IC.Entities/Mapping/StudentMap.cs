using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;
using Repository.Pattern.Annotations;

namespace IC.Entities.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            HasKey(student => student.StudentId);

            Property(student => student.StudentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(student => student.FirstName).IsRequired();
            Property(student => student.MiddleName).IsRequired();
            Property(student => student.LastName).IsRequired();
            Property(student => student.YearOfEntrance).IsRequired();
            Property(student => student.Login).IsRequired();
            Property(student => student.Password).IsRequired();

            HasRequired(entity => entity.Group)
                .WithMany(group => group.Students)
                .HasForeignKey(student => student.GroupId);
        }
    }
}
