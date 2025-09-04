using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Students.Commands.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Service.Abstracts;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrj.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler
        , IRequestHandler<AddStudentCommand, Response<string>>
        , IRequestHandler<EditStudentCommand, Response<string>>
        , IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result =await _studentService.AddStudentAsync(studentMapper);
             if (result == "Success")
            {
                return Created("");
            }
            else
            {
                return BadRequest<string>("Failed to Add");
            }

        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student =await _studentService.GetStudentwithExcludeByIdAsync(request.Id);
            if (student == null) return NotFound<string>("Student Not Found");
            var studentMapper = _mapper.Map(request,student);
            var result =await _studentService.EditStudentAsync(studentMapper);
            if (result == "Success")
            {
                return Success("Edit Successfully");
            }
            else
            {
                return BadRequest<string>("Failed to Add");
            }
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentwithExcludeByIdAsync(request.Id);
            if (student == null) return NotFound<string>("Student Not Found");
            var result =await _studentService.DeleteStudentAsync(student);
            if (result == "Success")
            {
                return Deleted<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            }
            else
            {
                return BadRequest<string>("Failed to Add");
            }
        }
    }
}
