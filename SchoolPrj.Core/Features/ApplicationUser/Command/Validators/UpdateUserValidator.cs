using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.ApplicationUser.Command.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UpdateUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
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

        }

    }
}

