using EducationalPortal.Database.Abstractions;
using EducationalPortal.Database.Enums;
using EducationalPortal.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalPortal.Database
{
    public class AppDbContext : DbContext
    {
        //public AppDBContext() : base()
        //{
        //    Database.Migrate();
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EducationalYearModel> EducationalYears { get; set; }
        public DbSet<GradeModel> Grades { get; set; }
        //public DbSet<PermisionTeacherEditSubjectModel> PermisionTeachersEditSubjects { get; set; }
        public DbSet<SettingModel> Settings { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<SubjectPostModel> SubjectPost { get; set; }
        public DbSet<UserModel> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    if (!options.IsConfigured)
        //    {
        //        options.UseMySQL("server=localhost;database=educational-portal;user=root;password=;port=3306;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EducationalYearModel>(y => y.HasIndex(e => e.Name).IsUnique());
            //builder.Entity<EducationalYearModel>().HasMany(y => y.Subjects).WithOne(s => s.EducationalYear).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GradeModel>(g => g.HasIndex(e => e.Name).IsUnique());
            builder.Entity<GradeModel>().HasMany(g => g.Students).WithOne(s => s.Grade).OnDelete(DeleteBehavior.SetNull);

            //builder.Entity<PermisionTeacherEditSubjectModel>().HasOne(p => p.Subject).WithMany(s => s.PermisionTeachersEditSubject).OnDelete(DeleteBehavior.SetNull);
            //builder.Entity<PermisionTeacherEditSubjectModel>().HasOne(p => p.Teacher).WithMany(t => t.PermisionTeachersEditSubject).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<SubjectModel>().HasOne(s => s.EducationalYear).WithMany(y => y.Subjects).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<UserModel>(u => u.HasIndex(e => e.Login).IsUnique());
            builder.Entity<UserModel>(u => u.HasIndex(e => e.Email).IsUnique());
            builder.Entity<UserModel>().Property(u => u.Role).HasConversion(r => r.ToString(), r => (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), r));
            builder.Entity<UserModel>().HasOne(p => p.Grade).WithMany(g => g.Students).OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                DateTime now = DateTime.Now;
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedAt = now;
                    ((BaseModel)entity.Entity).Id = Guid.NewGuid();
                }
                ((BaseModel)entity.Entity).UpdatedAt = now;
            }
        }

     
    }
}
