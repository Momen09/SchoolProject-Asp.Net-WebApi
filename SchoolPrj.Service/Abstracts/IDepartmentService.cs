

using SchoolProject.Data.Entites;

namespace SchoolPrj.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentNameById(int id);
    }
}
