

using SchoolPrj.Core.Features.Depatment.Queries.Results;
using SchoolPrj.Data.Entites;
using SchoolProject.Data.Entites;

namespace SchoolPrj.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetByIdDepartmentResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
            .ForMember(dest => dest.SubjectsList, opt => opt.MapFrom(src => src.DepartmentSubjects))
            .ForMember(dest => dest.InstructorsList, opt => opt.MapFrom(src => src.Instructors))
            .ForMember(dest => dest.StudentsList, opt => opt.MapFrom(src => src.Students));
            
            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Subject.SubId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameEn, src.Subject.SubjectNameAr)));
        
            CreateMap<Student, StudentResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));
        }
    }
}
