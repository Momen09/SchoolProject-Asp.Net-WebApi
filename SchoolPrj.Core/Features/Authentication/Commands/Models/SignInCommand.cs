using MediatR;
using SchoolPrj.Core.Bases;
using SchoolPrj.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
