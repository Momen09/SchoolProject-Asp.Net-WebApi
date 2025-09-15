using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
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
        public void AddUserCommandMapping()
        {
            CreateMap<AddUserCommand, User>();
            //       ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            //.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
