using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Service.Abstracts;
using SchoolProject.Infrastructure.Data;

namespace SchoolPrj.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUrlHelper _urlHelper;

        public ApplicationUserService(
              IEmailService emailService
            , IUrlHelper urlHelper
            , ApplicationDbContext applicationDbContext
            , UserManager<User> userManager
            , IHttpContextAccessor httpContextAccessor
            )
        {
            _urlHelper = urlHelper;
            _applicationDbContext = applicationDbContext;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<string> AddUserAsync(User user, string password)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                if (existUser != null) return "EmailIsExist";

                var userByUsername = await _userManager.FindByNameAsync(user.UserName);
                if (userByUsername != null) return "UserNameIsExist";
                var createUser = await _userManager.CreateAsync(user, password);
                if (!createUser.Succeeded) return string.Join(",",createUser.Errors.Select(x=>x.Description).ToList());
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +_urlHelper.Action("ConfirmEmail", "Authentication",new {userId=user.Id,code =code}) ;
                    //"/" + "api/Account/Authentication/ConfirmEmail" + "?userId=" + user.Id + "&code=" + code;
                await _emailService.SendEmail(user.Email, returnUrl);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }


        }
    }
}
