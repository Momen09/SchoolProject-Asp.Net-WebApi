using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolPrj.Service.AuthService.Interfaces;

namespace SchoolPrj.Core.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        private readonly ICurrentUserService _currentUserService;
        public AuthFilter
            (
            ICurrentUserService currentUserService
            )
        {
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated == true)
            {
                //var user =await _currentUserService.GetUserAsync();
                var roles = await _currentUserService.GetCurrentUserRolesAsync();
                if (roles.All(x => x != "User"))
                {
                    context.Result = new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
