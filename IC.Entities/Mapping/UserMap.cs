using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IC.Entities.Models;

namespace IC.Entities.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(user => user.UserId);

            Property(user => user.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(user => user.FirstName).IsRequired();
            Property(user => user.SecondName).IsRequired();
            Property(user => user.Email).IsRequired();
            Property(user => user.PasswordHash).IsRequired();

            ToTable("Users");

            HasMany(entity => entity.UserRoles);
        }
    }
}
