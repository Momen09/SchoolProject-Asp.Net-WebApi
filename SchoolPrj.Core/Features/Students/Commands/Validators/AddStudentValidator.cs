using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Features.Students.Commands.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Service.Abstracts;

namespace SchoolPrj.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        public AddStudentValidator(
            IDepartmentService departmentService,
            IStudentService studentService,
            IStringLocalizer<SharedResources> stringLocalizer
            )
        {
            _departmentService = departmentService;
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
                .MaximumLength(10).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength]);

            RuleFor(x => x.Address)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required])
               .MaximumLength(10).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength]);

            RuleFor(x => x.DepartmentId)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameAr)
                    .MustAsync(async (Key, CancellationToken) => 
                    !await _studentService.IsNameExist(Key))
                    .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);
            RuleFor(x => x.NameEn)
                       .MustAsync(async (Key, CancellationToken) => 
                       !await _studentService.IsNameExist(Key))
                       .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);

            
                RuleFor(x => x.DepartmentId)
                       .MustAsync(async (Key, CancellationToken) => 
                      await _departmentService.IsDepartmentIdexist(Key))
                       .WithMessage(_stringLocalizer[SharedResourcesKeys.DepartmentIdIsNotexist]);            
        }

    }
}
