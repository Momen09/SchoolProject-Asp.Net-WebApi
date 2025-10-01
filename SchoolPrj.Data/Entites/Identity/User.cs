using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolPrj.Data.Entites.Identity
{
    public class User :IdentityUser<int>
    {
        public User()
        {
            userRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [InverseProperty("user")]
        public virtual ICollection<UserRefreshToken> userRefreshTokens { get; set; }
    }
}
