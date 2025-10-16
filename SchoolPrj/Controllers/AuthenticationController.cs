using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.Authentication.Commands.Models;
using SchoolPrj.Core.Features.Authentication.Queries.Handlers;
using SchoolPrj.Core.Features.Authentication.Queries.Models;
using SchoolPrj.Core.Features.Students.Commands.Models;
using SchoolPrj.Data.AppMetaData;

namespace SchoolPrj.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.Auth.signIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPost(Router.Auth.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPost(Router.Auth.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromForm] AuthorizeUserQuery command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
