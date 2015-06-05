using System;
using System.Data.Entity;
using System.Linq;
using IC.Configuration;
using IC.Entities.Models;
using IC.Helpers;
using IC.RandomInformation.RandomGenerators;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class StudentSeedFiller : AbstractSeedFiller<Student>
    {
        public StudentSeedFiller(IcContext context)
            : base(context)
        {
        }


        protected override DbSet<Student> GetBaseList()
        {
            return Context.Students;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.QuantityOfStudents;
        }

        public override Student GenerateEntity(int index)
        {
            var group = RandomGenerator.GetRandomValueFromList(Context.Groups.ToList());
            group.ObjectState = ObjectState.Modified;

            var student = new Student
            {
                GroupId = group.GroupId,
                FirstName = RandomGenerator.GetRandomFirstName(),
                MiddleName = RandomGenerator.GetRandomMiddleName(),
                LastName = RandomGenerator.GetRandomSecondName(),
                Password = RandomGenerator.GetRandomPassword(10, 15),
                YearOfEntrance = RandomGenerator.GetRandomValueFromMinToMax(DateTime.Now.Year - 6, DateTime.Now.Year + 5),
                ObjectState = ObjectState.Added
            };

            student.Login = GenerateLoginHelper.GenerateLogin(student.FirstName, student.MiddleName, student.LastName,
                student.YearOfEntrance);

            group.Students.Add(student);

            return student;
        }
    }
}
