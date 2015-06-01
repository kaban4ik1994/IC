using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IC.Configuration;
using IC.Entities.Models;
using IC.RandomInformation.RandomGenerators;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class GroupSeedFiller : AbstractSeedFiller<Group>
    {
        public GroupSeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<Group> GetBaseList()
        {
            return Context.Groups;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.QuantityOfGroups;
        }

        public override Group GenerateEntity(int index)
        {
            var specialty = RandomGenerator.GetRandomValueFromList(Context.Specialties.ToList());
            specialty.ObjectState = ObjectState.Modified;

            var group = new Group
            {
                SpecialtyId = specialty.SpecialtyId,
                GroupNumber = index + 1,
                Students = new List<Student>(),
                ObjectState = ObjectState.Added
            };

            specialty.Groups.Add(group);

            return group;
        }
    }
}
