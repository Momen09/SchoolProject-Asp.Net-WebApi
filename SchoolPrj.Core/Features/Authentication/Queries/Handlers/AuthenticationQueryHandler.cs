using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Authentication.Queries.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Service.Abstracts;
using SchoolPrj.Service.Implementations;

namespace SchoolPrj.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationQueryHandler(
            IAuthenticationService authenticationService,
            IStringLocalizer<SharedResources> stringLocalizer
              ) : base(stringLocalizer)
        {
            _authenticationService = authenticationService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result =await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired") return Success(result);
            return NotFound<string>(result);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail =await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (confirmEmail== "ErrorWhenConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorWhenConfirmEmail]);
               return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailIsDone]);

        }
    }
}
