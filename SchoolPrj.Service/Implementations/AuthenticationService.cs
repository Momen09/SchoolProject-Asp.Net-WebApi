using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Data.Helpers;
using SchoolPrj.Infrastructure.Abstracts;
using SchoolPrj.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static SchoolPrj.Data.Helpers.JwtAuthResult;

namespace SchoolPrj.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        //private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(
            JwtSettings jwtSettings,
            UserManager<User> userManager,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings;
            //_UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
        }
        public async Task<JwtAuthResult> GetJWTTokenAsync(User user)
        {
            var Token = GenerateJWTToken(user);
            var accesstoken = new JwtSecurityTokenHandler().WriteToken(Token);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = accesstoken,
                RefreshToken = refreshToken.TokenString,
                JwtId = Token.Id,
                IsUsed = true,
                IsRevoked = false,
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiration)
            };
           var createResult=  await _refreshTokenRepository.AddAsync(userRefreshToken); 
            return new JwtAuthResult
            {
                AccessToken = accesstoken,
                refreshToken = refreshToken
            };
        }

        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {

                TokenString = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiration),
                Username = userName
            };
            //_UserRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, oldValue) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private JwtSecurityToken GenerateJWTToken(User user)
        {
            var claim = GetClaims(user);
            var Token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
                );
            return Token;
        }
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),
            };
            return claims;
        }


        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            var jwtToken = ReadJWTToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Algorithm is wrong");
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token not expired yet");
            }

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken && r.Token == accessToken && r.UserId== int.Parse(userId));
            if (userRefreshToken == null)
            {
                throw new SecurityTokenException("Invalid refresh token or access token.");
            }
            if(userRefreshToken.ExpiryDate<DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh token has expired.");
            }
            var user= await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SecurityTokenException("User not found.");
            }
            // Replace this line:
            // var (jwtSecurityToken,newToken) = GenerateJWTToken(user);

            // With the following two lines:
            var jwtSecurityToken = GenerateJWTToken(user);
            var newToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //var (jwtSecurityToken,newToken) = GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.Username = jwtToken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString=refreshToken;
            refreshTokenResult.ExpiresAt = userRefreshToken.ExpiryDate;
            response.refreshToken = refreshTokenResult;
            return response;
        }

        //not the best way to do it
        //public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        //{
        //    var jwtToken = ReadJWTToken(accessToken);
        //    if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        //    {
        //        throw new SecurityTokenException("Algorithm is wrong");
        //    }
        //    if (jwtToken.ValidTo > DateTime.UtcNow)
        //    {
        //        throw new SecurityTokenException("Token not expired yet");
        //    }
        //    var user = await _refreshTokenRepository.GetTableNoTracking()
        //        .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken && r.Token == accessToken);
        //    var username = jwtToken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.UserName))?.Value;

        //    // You need to return a JwtAuthResult object here.
        //    // If user is null, you may want to handle that case as well.
        //    if (user == null)
        //    {
        //        throw new SecurityTokenException("Invalid refresh token or access token.");
        //    }

        //    // Generate new tokens for the user
        //    var newJwtAuthResult = await GetJWTTokenAsync(new User
        //    {
        //        Id = user.UserId,
        //        UserName = username,
        //        Email = "", // Fill as needed
        //        PhoneNumber = "" // Fill as needed
        //    });

        //    return newJwtAuthResult;
        //}
        private JwtSecurityToken ReadJWTToken (string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            return jwtToken;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                //ClockSkew = TimeSpan.Zero
            };
            var validator = tokenHandler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try {
                if (validatedToken == null)
                {
                    throw new SecurityTokenException("Invalid Token");
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}