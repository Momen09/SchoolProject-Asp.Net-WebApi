using Microsoft.Extensions.DependencyInjection;
using SchoolPrj.Service.Abstracts;
using SchoolPrj.Service.Implementations;

namespace SchoolPrj.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
