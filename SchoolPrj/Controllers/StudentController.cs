using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.Students.Commands.Models;
using SchoolPrj.Core.Features.Students.Queries.Models;
using SchoolPrj.Data.AppMetaData;

namespace SchoolPrj.Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.Student.getList)]
        public async Task<IActionResult> GetStudentsListAsync() 
        {
            var result = await Mediator.Send(new GetStudentListQuery());
            return NewResult(result);
        }
        [HttpGet(Router.Student.paginatedList)]
        public async Task<IActionResult> PaginatedList([FromQuery] GetStudentPaginatedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet(Router.Student.getById)]
        public async Task<IActionResult> GetStudentsByIdAsync([FromRoute]int id)
        {
            var result = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(result);
        }

        [HttpPost(Router.Student.create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut(Router.Student.edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.Student.delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(result);
        }
    }
}
