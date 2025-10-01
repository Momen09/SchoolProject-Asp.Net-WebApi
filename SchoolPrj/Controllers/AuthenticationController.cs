using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.Authentication.Commands.Models;
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
    }
}
