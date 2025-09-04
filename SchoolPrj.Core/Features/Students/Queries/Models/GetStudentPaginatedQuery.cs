using MediatR;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Students.Queries.Results;
using SchoolPrj.Core.Wrappers;
using SchoolPrj.Data.Helpers;

namespace SchoolPrj.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
