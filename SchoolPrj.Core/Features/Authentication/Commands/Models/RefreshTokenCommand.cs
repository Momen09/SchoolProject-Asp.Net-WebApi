using MediatR;
using SchoolPrj.Core.Bases;
using SchoolPrj.Data.Helpers;


namespace SchoolPrj.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }    
    }
}
