

namespace SchoolPrj.Core.Features.Depatment.Queries.Results
{
    public class GetByIdDepartmentResponse
    {   public int Id { get; set; }
        public string Name { get; set; } 
        public string ManagerName { get; set; } 
        public List<StudentResponse>? StudentsList { get; set; }
        public List<SubjectResponse>? SubjectsList { get; set; }
        public List<InstructorResponse>? InstructorsList { get; set; }
    }
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class InstructorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
