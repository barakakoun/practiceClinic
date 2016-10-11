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
        public DbSet<Medicine> Medicines { get; set; }
        //public DbSet<PatientDrug> PatientDrugs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //builder.Entity<ProductCategory>().HasKey(x => new { x.ProductId, x.CategoryId });

            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(pc => pc.Product)
            //    .WithMany(p => p.ProductCategories)
            //    .HasForeignKey(pc => pc.ProductId);

            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(pc => pc.Category)
            //    .WithMany(c => c.ProductCategories)
            //    .HasForeignKey(pc => pc.CategoryId);



            //builder.Entity<Patient>()
            //     .HasMany(t => t.DrugAllergies)
            //     .WithMany(i => i.Patients)
            //     .Map(t => t.MapLeftKey("CourseID")
            //         .MapRightKey("InstructorID")
            //         .ToTable("CourseInstructor"));

            //builder.Entity<PatientDrug>()
            //    .HasKey(t => new { t.PatientID, t.DrugID });

            //builder.Entity<PatientDrug>()
            //    .HasOne(pt => pt.Patient)
            //    .WithMany(p => p.PatientDrugs)
            //    .HasForeignKey(pt => pt.PatientID);

            //builder.Entity<PatientDrug>()
            //    .HasOne(pt => pt.Drug)
            //    .WithMany(t => t.PatientDrugs)
            //    .HasForeignKey(pt => pt.DrugID);


            ////builder.conventions.remove<pluralizingtablenameconvention>();s

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
