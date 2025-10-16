using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Features.Emails.Commands.Models;
using SchoolPrj.Core.Resources;

namespace SchoolPrj.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SendEmailValidator(

            IStringLocalizer<SharedResources> stringLocalizer
            )
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Message)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
    }
}
