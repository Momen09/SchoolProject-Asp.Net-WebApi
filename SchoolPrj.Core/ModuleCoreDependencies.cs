using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolPrj.Core.Behaviors;
using System.Reflection;

namespace SchoolPrj.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // Configure MediatR to scan the current assembly for handlers, requests, and notifications
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configure AutoMapper to scan the current assembly for profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
