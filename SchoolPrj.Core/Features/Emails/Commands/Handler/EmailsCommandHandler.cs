


using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Bases;
using SchoolPrj.Core.Features.Emails.Commands.Models;
using SchoolPrj.Core.Resources;
using SchoolPrj.Service.Abstracts;

namespace SchoolPrj.Core.Features.Emails.Commands.Handler
{
    public class EmailsCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IEmailService _emailService;
        public EmailsCommandHandler(
            IMapper mapper, 
            IEmailService emailService,
            IStringLocalizer<SharedResources> stringLocalizer) 
            : base(stringLocalizer)
        {
            _emailService = emailService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response =await _emailService.SendEmail(request.Email, request.Message);
            if (response == "Success")
            {
                return Success("");
            }
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
        }
    }
}
