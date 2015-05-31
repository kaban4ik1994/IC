using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            HasKey(group => group.GroupId);

            Property(group => group.GroupId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(group => group.GroupNumber).IsRequired();

            ToTable("Groups");

            HasMany(entity => entity.Students);
            HasRequired(entity => entity.Specialty)
                .WithMany(specialty => specialty.Groups)
                .HasForeignKey(group => group.SpecialtyId);
        }
    }
}
