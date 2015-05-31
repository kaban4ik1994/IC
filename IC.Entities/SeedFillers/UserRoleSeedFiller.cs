﻿using System.Data.Entity;
using System.Linq;
using IC.Configuration;
using IC.Entities.Models;
using Repository.Pattern.Infrastructure;

namespace IC.Entities.SeedFillers
{
    public class UserRoleSeedFiller : AbstractSeedFiller<UserRole>
    {
        public UserRoleSeedFiller(IcContext context)
            : base(context)
        {
        }

        protected override DbSet<UserRole> GetBaseList()
        {
            return Context.UserRoles;
        }

        protected override int GetGenerationCount()
        {
            return Configurations.Roles.Count;
        }

        public override UserRole GenerateEntity(int index)
        {
            var user = Context.Set<User>().First();
            user.ObjectState = ObjectState.Modified;
            var roles = Context.Set<Role>().ToList();
            roles.ForEach(role => role.ObjectState = ObjectState.Modified);
            return new UserRole { Role = roles[index], User = user, ObjectState = ObjectState.Added };
        }
    }
}
