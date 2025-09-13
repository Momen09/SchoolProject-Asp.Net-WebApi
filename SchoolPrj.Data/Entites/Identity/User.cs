using Microsoft.AspNetCore.Identity;


namespace SchoolPrj.Data.Entites.Identity
{
    public class User :IdentityUser<int>
    {
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
