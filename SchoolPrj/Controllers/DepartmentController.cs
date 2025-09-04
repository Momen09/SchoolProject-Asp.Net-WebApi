using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Api.Base;
using SchoolPrj.Core.Features.Depatment.Queries.Models;
using SchoolPrj.Core.Features.Students.Queries.Models;
using SchoolPrj.Data.AppMetaData;

namespace SchoolPrj.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.Department.getById)]
        public async Task<IActionResult> GetDepartmentByIdAsync([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetByIdDepartmentQuery(id));
            return NewResult(result);
        }
    }
}
