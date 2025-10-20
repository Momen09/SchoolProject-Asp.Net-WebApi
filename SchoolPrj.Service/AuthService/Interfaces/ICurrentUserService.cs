using SchoolPrj.Data.Entites.Identity;

namespace SchoolPrj.Service.AuthService.Interfaces
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();

    }
}
