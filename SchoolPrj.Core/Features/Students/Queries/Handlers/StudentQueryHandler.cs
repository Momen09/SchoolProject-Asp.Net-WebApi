using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Students.Queries.Models;
using SchoolPrj.Core.Features.Students.Queries.Results;
using SchoolPrj.Core.Resources;
using SchoolPrj.Core.Wrappers;
using SchoolPrj.Service.Abstracts;
using SchoolProject.Data.Entites;
using System.Linq.Expressions;

namespace SchoolPrj.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler
        , IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>
        , IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>
        , IRequestHandler<GetStudentPaginatedQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetStudentsListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);
            var result = Success(studentListMapper);
            result.Meta = new { Count = studentListMapper.Count() };
            return result;
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentsWithIncludeByIdAsync(request.Id);
            if (student == null) return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var studentMapper = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(studentMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(
                e.StudentId
                , e.Localize(e.NameAr,e.NameEn),
                e.Address,
                e.Localize(e.Department.DNameAr, e.Department.DNameEn));
            //var querable = _studentService.GetStudentsQuerable();
            var filterQuery = _studentService.FilterStudentPaginatedQuerable(request.Search, request.OrderBy);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;
        }
    }
}
