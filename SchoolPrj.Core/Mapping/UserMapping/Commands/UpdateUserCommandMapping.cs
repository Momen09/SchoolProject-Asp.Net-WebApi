

using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Data.Entites.Identity;

namespace SchoolPrj.Core.Mapping.UserMapping
{
    public partial class ApplicationUserProfile
    {
        public void UpdateUserCommandMapping()
        {
            CreateMap<UpdateUserCommand, User>();

        }
    }
}
