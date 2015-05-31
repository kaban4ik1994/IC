using System.Data.Entity;
using IC.Configuration;
using IC.Entities.Models;
using IC.Helpers;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class UserSeedFiller : AbstractSeedFiller<User>
    {
        public UserSeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<User> GetBaseList()
        {
            return Context.Users;
        }

        protected override int GetGenerationCount()
        {
            return 1;
        }

        public override User GenerateEntity(int index)
        {
            return new User
            {
                FirstName = Configurations.AdminFirstName,
                SecondName = Configurations.AdminSecondName,
                Email = Configurations.AdminEmail,
                PasswordHash = PasswordHashHelper.GetHash(Configurations.AdminPassword),
                ObjectState = ObjectState.Added
            };
        }
    }
}
