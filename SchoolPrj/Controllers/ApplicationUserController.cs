using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Data.AppMetaData;

namespace SchoolPrj.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.User.create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
