
using Microsoft.EntityFrameworkCore;
using SchoolPrj.Infrastructure.Abstracts;
using SchoolPrj.Service.Abstracts;
using SchoolProject.Data.Entites;

namespace SchoolPrj.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository )
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Department> GetDepartmentNameById(int id)
        {
           var student = await _departmentRepository.GetTableNoTracking()
                .Where(x=>x.DID==id)
                .Include(x=>x.DepartmentSubjects).ThenInclude(x=>x.Subject)
                .Include(x=>x.Students)
                .Include(x=>x.Instructors)
                .Include(x=>x.Instructor)
                .FirstOrDefaultAsync();
            return student;
        }
    }
}
