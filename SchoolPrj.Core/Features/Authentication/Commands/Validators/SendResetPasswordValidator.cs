using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Features.Authentication.Commands.Models;
using SchoolPrj.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.Authentication.Commands.Validators
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SendResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
        }
    }
}
