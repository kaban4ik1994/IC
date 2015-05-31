using System.Data.Entity;
using IC.Configuration;
using IC.Entities.Models;
using IC.RandomInformation.RandomGenerators;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class ComputerSeedFiller : AbstractSeedFiller<Computer>
    {
        public ComputerSeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<Computer> GetBaseList()
        {
            return Context.Computers;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.QuantityOfComputers;
        }

        public override Computer GenerateEntity(int index)
        {
            return new Computer
            {
                Name = RandomGenerator.GetRandomStringFromMinCountToMaxCount(5, 15),
                IpAddress = RandomGenerator.GetRandomIpAddress(),
                NetworkAddress = RandomGenerator.GetRandomIpAddress(),
                SubnetAddress = RandomGenerator.GetRandomIpAddress(),
                RoomNumber = RandomGenerator.GetRandomValueFromMinToMax(1, 320),
                ObjectState = ObjectState.Added
            };
        }
    }
}
