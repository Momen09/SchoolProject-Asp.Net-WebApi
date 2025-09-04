using SchoolPrj.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entites;

namespace SchoolPrj.Core.Mapping.StudentMap
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>().
                                ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId)).
                                ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id))
                                 .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                                 .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));


        }
    }
}
