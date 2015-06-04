using System.Data.Entity;
using IC.Entities.Models;
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

            //var computerFiller = new ComputerSeedFiller(context);
            //computerFiller.Fill();

            var courseFiller = new CourseSeedFiller(context);
            courseFiller.Fill();

            var specialtyFiller = new SpecialtySeedFiller(context);
            specialtyFiller.Fill();

            var groupFiller = new GroupSeedFiller(context);
            groupFiller.Fill();

            var studentFiller = new StudentSeedFiller(context);
            studentFiller.Fill();

            base.Seed(context);
        }
    }
}
