using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using WebApplication3.Models;
//using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication3.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Fan> Fan { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Drug> Drugs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ////builder.conventions.remove<pluralizingtablenameconvention>();s

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
