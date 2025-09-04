using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPrj.Data.Entites;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Infrastructure.Configurations
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(x => new { x.SubID, x.StudID });

            builder.HasOne(ds => ds.Student)
               .WithMany(d => d.StudentSubject)
               .HasForeignKey(ds => ds.StudID);

            builder.HasOne(ds => ds.Subject)
            .WithMany(d => d.StudentSubjects)
            .HasForeignKey(ds => ds.SubID);
        }

    }
}
