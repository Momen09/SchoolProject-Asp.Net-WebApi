using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Depatment.Queries.Models;
using SchoolPrj.Core.Features.Depatment.Queries.Results;
using SchoolPrj.Core.Resources;
using SchoolPrj.Service.Abstracts;

namespace SchoolPrj.Core.Features.Depatment.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler
        , IRequestHandler<GetByIdDepartmentQuery, Response<GetByIdDepartmentResponse>>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer
            ,IDepartmentService departmentService
            ,IMapper mapper) : base(stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }

        public async Task<Response<GetByIdDepartmentResponse>> Handle(GetByIdDepartmentQuery request, CancellationToken cancellationToken)
        {
            var response =await _departmentService.GetDepartmentNameById(request.Id);
            if (response == null)
                return NotFound<GetByIdDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var mapper = _mapper.Map<GetByIdDepartmentResponse>(response);
            return Success(mapper);
        }
    }
}
