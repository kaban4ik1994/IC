using System.Data.Entity;
using IC.Configuration;
using IC.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class RoleSeedFiller : AbstractSeedFiller<Role>
    {
        public RoleSeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<Role> GetBaseList()
        {
            return Context.Roles;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.Roles.Count;
        }

        public override Role GenerateEntity(int index)
        {
            var list = Configurations.Roles;
            return new Role { Name = list[index], ObjectState = ObjectState.Added };
        }
    }
}
