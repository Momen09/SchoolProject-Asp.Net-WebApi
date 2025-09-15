using MediatR;
using SchoolPrj.Core.Features.ApplicationUser.Queries.Results;
using SchoolPrj.Core.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginationListQuery : IRequest<PaginatedResult<GetUserPaginationListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchString { get; set; }
    }
}
