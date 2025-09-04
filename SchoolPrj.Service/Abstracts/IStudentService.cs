using SchoolPrj.Data.Helpers;
using SchoolProject.Data.Entites;
namespace SchoolPrj.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync(); 
        public IQueryable<Student> GetStudentsQuerable();
        public IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum orderingEnum);
        public Task<Student> GetStudentsWithIncludeByIdAsync(int id);
        public Task<Student> GetStudentwithExcludeByIdAsync(int id);
        public Task<string> AddStudentAsync(Student student);
        public Task<bool> IsNameExist(string name);
        public Task<string> EditStudentAsync(Student student);
        public Task<bool> IsNameExistExcludeSelf(string name,int Id);
        public Task<string> DeleteStudentAsync(Student student);


    }
}
