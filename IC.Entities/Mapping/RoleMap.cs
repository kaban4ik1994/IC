using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(role => role.RoleId);

            Property(role => role.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(role => role.Name).IsRequired();

            ToTable("Roles");

            HasMany(entity => entity.UserRoles);
        }
    }
}
