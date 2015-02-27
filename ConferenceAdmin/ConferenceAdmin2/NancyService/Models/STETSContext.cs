using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NancyService.Models
{
    public class STETSContext: DbContext, NancyService.Models.ISTETSContext
    {
        public STETSContext():
            base("STETSDbConnection")
        {
            Database.SetInitializer<STETSContext>(new STETSDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class STETSDbInitializer : CreateDatabaseIfNotExists<STETSContext>
    {
        // Dummy data creation to test the log in by default.
        protected override void Seed(STETSContext context)
        {
            context.Roles.Add(new Role() {RoleId=1,Name = "Admin", Value = "admin" });

            IList<User> defaultUsers = new List<User>();

            defaultUsers.Add(new User() {UserId=1,FirstName = "Vicente", LastName = "Rivera", Age = 30, Email = "vriveras@gmail.com" });
            foreach (var user in defaultUsers)
            {
                context.Users.Add(user);
            }

            context.Claims.Add(new Claim() { ClaimId=1,RoleId = 1, UserId = 1 });

            base.Seed(context);
        }
    }
}