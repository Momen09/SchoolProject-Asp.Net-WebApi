using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.ApplicationUser.Queries.Models;
using SchoolPrj.Core.Features.ApplicationUser.Queries.Results;
using SchoolPrj.Core.Resources;
using SchoolPrj.Core.Wrappers;
using SchoolPrj.Data.Entites.Identity;


namespace SchoolPrj.Core.Features.ApplicationUser.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginationListQuery, PaginatedResult<GetUserPaginationListResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer
            , IMapper mapper
            , UserManager<User> userManager
            ) : base(stringLocalizer)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<GetUserPaginationListResponse>> Handle(GetUserPaginationListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList =await _mapper.ProjectTo<GetUserPaginationListResponse>(users)
                .ToPaginatedListAsync(request.PageNumber,request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id);
            //var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var mappedUser = _mapper.Map<GetUserByIdResponse>(user);
            return Success(mappedUser);
        }
    }
}
