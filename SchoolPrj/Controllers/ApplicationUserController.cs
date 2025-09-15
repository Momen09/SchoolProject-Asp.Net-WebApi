using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Core.Features.ApplicationUser.Queries.Handler;
using SchoolPrj.Core.Features.ApplicationUser.Queries.Models;
using SchoolPrj.Core.Features.Students.Commands.Models;
using SchoolPrj.Core.Features.Students.Queries.Models;
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
        [HttpGet(Router.User.paginatedList)]
        public async Task<IActionResult> PaginatedList([FromQuery] GetUserPaginationListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet(Router.User.getById)]
        public async Task<IActionResult> GetStudentsByIdAsync([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(result);
        }
        [HttpPut(Router.User.update)]
        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
