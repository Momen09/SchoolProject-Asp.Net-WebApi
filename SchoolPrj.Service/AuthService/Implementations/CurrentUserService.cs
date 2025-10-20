using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Data.Helpers;
using SchoolPrj.Service.AuthService.Interfaces;

namespace SchoolPrj.Service.AuthService.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public CurrentUserService
            (
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUserAsync()
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return user;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == nameof(UserClaimModel.Id)).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userId);
        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetUserAsync();
            var roles =await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
