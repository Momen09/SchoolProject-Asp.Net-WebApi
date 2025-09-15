using MediatR;
using SchoolPrj.Core.Bases;


namespace SchoolPrj.Core.Features.ApplicationUser.Command.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
