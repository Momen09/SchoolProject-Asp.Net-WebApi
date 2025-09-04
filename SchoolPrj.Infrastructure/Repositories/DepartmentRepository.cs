using Microsoft.EntityFrameworkCore;
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
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        public readonly DbSet<Department> _departments;
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _departments= dbContext.Set<Department>();
        }
    }
}
