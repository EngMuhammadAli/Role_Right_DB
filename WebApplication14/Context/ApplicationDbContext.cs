using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication14.Models;
using WebApplication14.Models.RoleModel;
using WebApplication14.Models.UserModel;

namespace WebApplication14.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FormAccess> FormAccesses { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Student> Students { get; set; } 
    }
}
