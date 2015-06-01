using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IC.Configuration;
using IC.Entities.Models;
using IC.RandomInformation.RandomGenerators;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class SpecialtySeedFiller : AbstractSeedFiller<Specialty>
    {
        public SpecialtySeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<Specialty> GetBaseList()
        {
            return Context.Specialties;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.QuantityOfSpecialities;
        }

        public override Specialty GenerateEntity(int index)
        {
            var course = RandomGenerator.GetRandomValueFromList(Context.Courses.ToList());
            course.ObjectState = ObjectState.Modified;

            var specialty = new Specialty
            {
                CourseId = course.CourseId,
                Name = RandomGenerator.GetRandomSpecialtyName(),
                Groups = new List<Group>(),
                ObjectState = ObjectState.Added
            };
            
            course.Specialties.Add(specialty);

            return specialty;
        }
    }
}
