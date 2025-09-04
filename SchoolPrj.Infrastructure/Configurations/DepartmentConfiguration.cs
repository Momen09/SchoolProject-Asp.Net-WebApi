using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entites;

namespace SchoolPrj.Infrastructure.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(x => x.DID);
            builder.Property(x => x.DNameAr).HasMaxLength(200);

            builder.HasMany(d => d.Students)
                .WithOne(s => s.Department)
                .HasForeignKey(s => s.DID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Instructor)
                    .WithOne(i => i.DepartmentManager)
                    .HasForeignKey<Department>(d => d.InsManager)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
