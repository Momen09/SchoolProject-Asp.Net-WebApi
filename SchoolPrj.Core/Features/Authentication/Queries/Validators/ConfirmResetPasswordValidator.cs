

using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Features.Authentication.Queries.Models;
using SchoolPrj.Core.Resources;

namespace SchoolPrj.Core.Features.Authentication.Queries.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
    }
}
