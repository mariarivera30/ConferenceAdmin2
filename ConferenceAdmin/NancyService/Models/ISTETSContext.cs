using System;
using System.Data.Entity.Infrastructure;
namespace NancyService.Models
{
    public interface ISTETSContext
    {
        System.Data.Entity.DbSet<Claim> Claims { get; set; }
        System.Data.Entity.DbSet<Role> Roles { get; set; }
        System.Data.Entity.DbSet<User> Users { get; set; }
        
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}
