using ApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.DbContexts
{
    public class PermissionDbContext : DbContext
    {
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }

        public PermissionDbContext(DbContextOptions<PermissionDbContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }

        public List<PermissionType> GetPermissionTypes() => PermissionType.ToList<PermissionType>();
        public List<Permission> GetPermissions() => Permission.Include(x => x.PermissionType).ToList<Permission>();
        public Permission Find(int id) => Permission.Include(x => x.PermissionType).Where( x=> x.IdPermission == id).Single();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasOne(x => x.PermissionType).WithMany();

            //modelBuilder.Entity<Permission>().Property<int>("PermissionTypeForeignKey");

            //modelBuilder.Entity<Permission>().HasOne(x => x.PermissionType).WithMany().HasForeignKey("PermissionTypeForeignKey");
        }

        //private void LoadDefaultUsers()
        //{
        //    Permission.Add(
        //        new Permission {
        //            IdPermission =0,
        //            PersonName = "Juan Diego",
        //            LastName = "Sanchez", 
        //            PermissionType = new PermissionType
        //            {
        //                IdPermissionType=0,
        //                DescriptionPermission="Descripción"
        //            },
        //            DatePermission = DateTime.UtcNow

        //        });
        //}
    }
}


