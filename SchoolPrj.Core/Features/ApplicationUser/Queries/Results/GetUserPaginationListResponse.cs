using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.ApplicationUser.Queries.Results
{
    public class GetUserPaginationListResponse
    {   
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }


    }
}
