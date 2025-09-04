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
    public class InsSubjectConfiguration : IEntityTypeConfiguration<InsSubject>
    {
        public void Configure(EntityTypeBuilder<InsSubject> builder)
        {
            builder.HasKey(x => new { x.SubId, x.InsId });

            builder.HasOne(ds => ds.instructor)
                .WithMany(d => d.InsSubjects)
                .HasForeignKey(ds => ds.InsId);

            builder.HasOne(ds => ds.Subject)
            .WithMany(d => d.InsSubjects)
            .HasForeignKey(ds => ds.SubId);
        }

    }
}
