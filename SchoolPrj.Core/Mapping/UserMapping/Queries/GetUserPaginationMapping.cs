using SchoolPrj.Core.Features.ApplicationUser.Queries.Results;
using SchoolPrj.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Mapping.UserMapping
{
    public partial class ApplicationUserProfile
    {
        public void GetUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginationListResponse>();
        }
    }
}
