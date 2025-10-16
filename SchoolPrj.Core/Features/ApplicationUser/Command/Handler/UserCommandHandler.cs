using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Data.Entites.Identity;
using SchoolPrj.Service.Abstracts;


namespace SchoolPrj.Core.Features.ApplicationUser.Command.Handler
{
    public class UserCommandHandler : ResponseHandler
        , IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<UpdateUserCommand, Response<string>>
        , IRequestHandler<DeleteUserCommand, Response<string>>
        , IRequestHandler<ChangeUserPasswordCommand, Response<string>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IApplicationUserService _applicationUserService;

        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer
            ,IMapper mapper
            , IApplicationUserService applicationUserService
            , IEmailService emailService
            , UserManager<User> userManager
            , IHttpContextAccessor httpContextAccessor
            ) :base(stringLocalizer)
        {
            _applicationUserService = applicationUserService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            var mapperUser = _mapper.Map<User>(request);
            var createUser =await _applicationUserService.AddUserAsync(mapperUser,request.Password);
            switch(createUser)
            {
                case "Created":
                    return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
                case "EmailIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                case "UsernameIsExist":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UsernameIsExist]);
                case "SendEmailFailed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
                    case "ErrorInCreateUser":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddUser]);
                    case "Failed":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryToRegisterAgain]);
                    case "Success":
                    return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
                    default: return BadRequest<string>(createUser);
            }
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>("");
            var userMap = _mapper.Map(request,oldUser);
            var userByUsername = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==userMap.UserName&&x.Id!=userMap.Id);
            if (userByUsername != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UsernameIsExist]);
            var updateUser = await _userManager.UpdateAsync(userMap);
            if (!updateUser.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            return Updated("");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>("");
            var deleteUser = await _userManager.DeleteAsync(user);
            if (!deleteUser.Succeeded) return BadRequest<string>(deleteUser.Errors.FirstOrDefault().Description);
            return Deleted<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>("");
            var changePassword =await _userManager.ChangePasswordAsync(user,request.CurrentPassword,request.NewPassword);
            if (!changePassword.Succeeded) return BadRequest<string>(changePassword.Errors.FirstOrDefault().Description);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.PasswordChanged]);
        }
    }
}
