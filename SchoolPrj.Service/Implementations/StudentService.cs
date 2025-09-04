using Microsoft.EntityFrameworkCore;
using SchoolPrj.Data.Helpers;
using SchoolPrj.Infrastructure.Abstracts;
using SchoolPrj.Service.Abstracts;
using SchoolProject.Data.Entites;
//using static SchoolPrj.Data.AppMetaData.Router;

namespace SchoolPrj.Service.Implementations
{
    public class StudentService : IStudentService
    {
        public readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            //if (student.StudentId != null) student.StudentId == null;
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<Student> GetStudentsWithIncludeByIdAsync(int id)
        {
            //var students = await _studentRepository.GetByIdAsync(id);
            var student = await _studentRepository.GetTableNoTracking()
                .Include(x => x.Department)
                .Where(x => x.StudentId == id)
                .FirstOrDefaultAsync();
            return student;
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            try
            {
                return await _studentRepository.GetStudentsAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "dsfsdfsdfs");
            }
        }

        public async Task<bool> IsNameExist(string nameEn)
        {
            var student = _studentRepository.GetTableNoTracking().Where(x => x.NameEn == nameEn).FirstOrDefault();
            if (student == null)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> IsNameExistExcludeSelf(string nameEn, int Id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn == nameEn && x.StudentId != Id).FirstOrDefaultAsync();
            if (student == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> EditStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteStudentAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }



        public async Task<Student> GetStudentwithExcludeByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student;
        }

        public IQueryable<Student> GetStudentsQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum orderingEnum)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudId:
                    querable = querable.OrderBy(x => x.StudentId);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DNameAr);
                    break;
                default:
                    break;
            }
            return querable;
        }
    }
}
