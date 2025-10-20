using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolPrj.Data.Entites.Identity;
using SchoolProject.Data.Entites;
using System.Reflection;

namespace SchoolProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
        <User,
        IdentityRole<int>, int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        public ApplicationDbContext()
        {
        
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options
            ) 
        {
            _encryptionProvider = new GenerateEncryptionProvider("d6fd5d782d93484c8ff25d3376fad442");
        }


        public DbSet<User> User { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<DepartmentSubject> departmentSubjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }
        public DbSet<UserRefreshToken> userRefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("School");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(_encryptionProvider);
        }
    }
}
