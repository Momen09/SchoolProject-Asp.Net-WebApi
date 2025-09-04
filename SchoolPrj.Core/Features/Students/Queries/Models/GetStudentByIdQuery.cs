using MediatR;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Students.Queries.Results;

namespace SchoolPrj.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
