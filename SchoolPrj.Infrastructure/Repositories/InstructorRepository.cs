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
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        public readonly DbSet<Instructor> _instructors;

        public InstructorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _instructors = dbContext.Set<Instructor>();
        }
    }
}
