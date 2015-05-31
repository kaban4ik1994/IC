using System.Data.Entity;
using IC.Entities.SeedFillers;

namespace IC.Entities
{
    public class IcContextInitializer : CreateDatabaseIfNotExists<IcContext>
    {
        protected override void Seed(IcContext context)
        {
            var roleFiller = new RoleSeedFiller(context);
            roleFiller.Fill();

            var userFiller = new UserSeedFiller(context);
            userFiller.Fill();

            var userRoleFiller = new UserRoleSeedFiller(context);
            userRoleFiller.Fill();

            base.Seed(context);
        }
    }
}
