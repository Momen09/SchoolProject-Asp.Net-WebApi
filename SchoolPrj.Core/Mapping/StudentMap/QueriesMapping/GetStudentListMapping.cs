using SchoolPrj.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entites;


namespace SchoolPrj.Core.Mapping.StudentMap
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>().
                ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Localize(src.Department.DNameAr, src.Department.DNameEn))).
                            ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn))).
                            ForMember(dest => dest.StudId, opt => opt.MapFrom(src => src.StudentId));

        }
    }
}
