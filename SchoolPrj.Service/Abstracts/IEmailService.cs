using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Service.Abstracts
{
    public interface IEmailService
    {
        public Task<string> SendEmail(string Email, string Message,string? reason);
    }
}
