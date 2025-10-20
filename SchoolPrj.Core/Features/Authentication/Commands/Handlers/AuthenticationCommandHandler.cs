using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Authentication.Commands.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Data.Helpers;
using SchoolPrj.Service.Abstracts;


namespace SchoolPrj.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
        IRequestHandler<SendResetPasswordCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly  SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer
            , IMapper mapper
            , IAuthenticationService authenticationService
            , UserManager<User> userManager
            
            , SignInManager<User> signInManager
            ) : base(stringLocalizer)
        {
            _authenticationService = authenticationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.UsernameIsNotExist]);
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //confrim email
            if (!user.EmailConfirmed) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PleaseConfirmYourEmail]);
            if (!user.EmailConfirmed) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
            if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            var result = await _authenticationService.GetJWTTokenAsync(user);
            return Success(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result =await _authenticationService.GetRefreshToken(request.AccessToken, request.RefreshToken);
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch(result)
            {
                case "User Not Found":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgainAnotherTime]);
                case "Error In Update User":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgainAnotherTime]);
                case "Success":
                    return Created<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                default: return BadRequest<string>(result);

            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Password, request.Email);
            switch (result)
            {
                case "User Not Found":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvalidCode]);
                case "Success":
                    return Created<string>(_stringLocalizer[SharedResourcesKeys.Success]);
                default: return BadRequest<string>(result);

            }
        }
    }
}
