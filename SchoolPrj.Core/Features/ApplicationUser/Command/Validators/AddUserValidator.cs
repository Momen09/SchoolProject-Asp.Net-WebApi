using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Core.Resources;

namespace SchoolPrj.Core.Features.ApplicationUser.Command.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.ConfirmPassword)
              .Equal(x=>x.Password).WithMessage(_stringLocalizer[SharedResourcesKeys.PasswordNotEqualConfirmPassword]);
              }

    }
}
