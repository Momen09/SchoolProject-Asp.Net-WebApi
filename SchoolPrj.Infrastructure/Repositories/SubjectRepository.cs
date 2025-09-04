using Microsoft.EntityFrameworkCore;
using SchoolPrj.Data.Entites;
using SchoolPrj.Infrastructure.Abstracts;
using SchoolPrj.Infrastructure.InfrastructureBases;
using SchoolProject.Data.Entites;
using SchoolProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Infrastructure.Repositories
{
    internal class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
    {
        public readonly DbSet<Subjects> _subjects;

        public SubjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subjects>();
        }
    }
}
