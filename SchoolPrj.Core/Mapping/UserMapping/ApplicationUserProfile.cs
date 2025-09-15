using AutoMapper;


namespace SchoolPrj.Core.Mapping.UserMapping
{
    public partial class ApplicationUserProfile : Profile
    {

        public ApplicationUserProfile()
        {
            AddUserCommandMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();
            UpdateUserCommandMapping();
        }
    }
}
