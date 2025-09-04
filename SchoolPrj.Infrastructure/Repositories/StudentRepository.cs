using Microsoft.EntityFrameworkCore;
using SchoolPrj.Infrastructure.Abstracts;
using SchoolPrj.Infrastructure.InfrastructureBases;
using SchoolProject.Data.Entites;
using SchoolProject.Infrastructure.Data;
namespace SchoolPrj.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        public readonly DbSet<Student> _students;
        public StudentRepository(ApplicationDbContext dbContext): base(dbContext)
        { 
            _students = dbContext.Set<Student>();
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
             return await _students.Include(x=>x.Department).ToListAsync(); 
        }
    }
}
