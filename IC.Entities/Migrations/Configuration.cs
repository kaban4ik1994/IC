using System.Data.Entity.Migrations;

namespace IC.Entities.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IcContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(IcContext context)
        {

        }
    }
}
