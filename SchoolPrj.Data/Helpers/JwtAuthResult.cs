using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Data.Helpers
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken refreshToken { get; set; }
        public class RefreshToken
        {
            public string Username { get; set; }
            public string TokenString { get; set; }
            public DateTime ExpiresAt { get; set; }
        }
    }
}
