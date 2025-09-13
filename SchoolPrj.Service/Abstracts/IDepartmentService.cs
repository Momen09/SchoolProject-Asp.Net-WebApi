

using SchoolProject.Data.Entites;

namespace SchoolPrj.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentNameById(int id);
        public Task<bool> IsDepartmentIdexist(int departmentId);
    }
}
