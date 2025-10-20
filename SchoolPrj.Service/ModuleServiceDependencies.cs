using Microsoft.Extensions.DependencyInjection;
using SchoolPrj.Service.Abstracts;
using SchoolPrj.Service.AuthService.Implementations;
using SchoolPrj.Service.AuthService.Interfaces;
using SchoolPrj.Service.Implementations;

namespace SchoolPrj.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IApplicationUserService,ApplicationUserService>();
            services.AddTransient<ICurrentUserService,CurrentUserService>();
            return services;
        }
    }
}
