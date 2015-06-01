using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using IC.Configuration;
using IC.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class CourseSeedFiller : AbstractSeedFiller<Course>
    {
        public CourseSeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<Course> GetBaseList()
        {
            return Context.Courses;
        }

        protected override int GetGenerationCount()
        {
            return 6;
        }

        public override Course GenerateEntity(int index)
        {
            return new Course
            {
                CourseNumber = index + 1, ObjectState = ObjectState.Added, Specialties = new List<Specialty>()
            };
        }
    }
}
