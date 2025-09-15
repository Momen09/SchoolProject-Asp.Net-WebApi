using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Data.Entites.Identity;


namespace SchoolPrj.Core.Features.ApplicationUser.Command.Handler
{
    public class UserCommandHandler : ResponseHandler
        , IRequestHandler<AddUserCommand, Response<string>>
        , IRequestHandler<UpdateUserCommand, Response<string>>
        , IRequestHandler<DeleteUserCommand, Response<string>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer
            ,IMapper mapper
            , UserManager<User> userManager
            ) :base(stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);

            var userByUsername = await _userManager.FindByNameAsync(request.UserName);
            if (userByUsername != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UsernameIsExist]);
            var mapperUser = _mapper.Map<User>(request);
            var createUser =await _userManager.CreateAsync(mapperUser,request.Password);
            if (!createUser.Succeeded) return BadRequest<string>(createUser.Errors.FirstOrDefault().Description);
            return Created("");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>("");
            var userMap = _mapper.Map(request,oldUser);
            var updateUser = await _userManager.UpdateAsync(userMap);
            if (!updateUser.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            return Updated("");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>("");
            var deleteUser = await _userManager.DeleteAsync(user);
            if (!deleteUser.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            return Deleted<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
    }
}
