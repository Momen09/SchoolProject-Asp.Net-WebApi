using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Data.Helpers;


namespace SchoolPrj.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTTokenAsync(User user);
        public Task<JwtAuthResult> GetRefreshToken(string accessToken,string refreshToken);
        public Task<string> ValidateToken(string accessToken);
    }
}
