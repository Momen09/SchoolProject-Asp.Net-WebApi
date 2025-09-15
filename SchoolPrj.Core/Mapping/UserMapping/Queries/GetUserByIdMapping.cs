using SchoolPrj.Core.Features.ApplicationUser.Queries.Results;
using SchoolPrj.Data.Entites.Identity;


namespace SchoolPrj.Core.Mapping.UserMapping
{
    public partial class ApplicationUserProfile
    {
        private void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
