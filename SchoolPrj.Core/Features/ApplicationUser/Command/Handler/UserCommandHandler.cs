using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.ApplicationUser.Command.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.ApplicationUser.Command.Handler
{
    public class UserCommandHandler : ResponseHandler
        , IRequestHandler<AddUserCommand, Response<string>>

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
    }
}
