

using MediatR;
using SchoolPrj.Core.Bases;

namespace SchoolPrj.Core.Features.Authentication.Queries.Models
{
    public class ConfirmResetPasswordQuery : IRequest<Response<string>>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
