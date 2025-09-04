using MediatR;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Students.Queries.Results;
using System.Collections.Generic;

namespace SchoolPrj.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {
        // Add any properties or constructors if needed
    }
}
