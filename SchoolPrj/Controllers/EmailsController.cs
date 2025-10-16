using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.Emails.Commands.Models;
using SchoolPrj.Data.AppMetaData;

namespace SchoolPrj.Api.Controllers
{
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        [HttpPost(Router.EmailsRoute.sendEmail)]
        public async Task<IActionResult> sendEmail([FromForm] SendEmailCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
