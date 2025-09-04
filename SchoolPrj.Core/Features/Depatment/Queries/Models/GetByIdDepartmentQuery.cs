using MediatR;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Depatment.Queries.Results;

namespace SchoolPrj.Core.Features.Depatment.Queries.Models
{
    public class GetByIdDepartmentQuery : IRequest<Response<GetByIdDepartmentResponse>>
    {
        public int Id { get; set; }
        public GetByIdDepartmentQuery(int id)
        {
            Id = id;
        }
    }
}
