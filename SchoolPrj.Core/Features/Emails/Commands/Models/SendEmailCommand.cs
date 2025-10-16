using MediatR;
using SchoolPrj.Core.Bases;


namespace SchoolPrj.Core.Features.Emails.Commands.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
