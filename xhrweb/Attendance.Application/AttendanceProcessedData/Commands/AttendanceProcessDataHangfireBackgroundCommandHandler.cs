using Attendance.Core.Entities;
using Attendance.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Attendance.Application.AttendanceProcessedData.Commands.Commands.V1;

namespace Attendance.Application.AttendanceProcessedData.Commands
{
    public class AttendanceProcessDataHangfireBackgroundCommandHandler : IRequestHandler<AttendanceProcessDataHangfireBackgroundCommand, List<AttendanceProcessedHangfireBackgroundCommandVM>>
    {
        public AttendanceProcessDataHangfireBackgroundCommandHandler(IReferenceRepository<Company, Guid> companyRepository, IMediator mediator)
        {
            _mediator = mediator;
            _companyRepository = companyRepository;
        }

        private readonly IMediator _mediator;
        private readonly IReferenceRepository<Company, Guid> _companyRepository;

        public async Task<List<AttendanceProcessedHangfireBackgroundCommandVM>> Handle(AttendanceProcessDataHangfireBackgroundCommand request, CancellationToken cancellationToken)
        {
            var result = new List<AttendanceProcessedHangfireBackgroundCommandVM>();

            //Azhar vi will finish it. He will make a query to get all active company from db.
            //var activeCompanyIds = new List<Guid> {
            //    new Guid("ab5aeca2-7a4a-4a20-bb96-383e72e839dc"),
            //    new Guid("66986982-28b4-419b-bdfd-d74bd556ce45")
            //};

            var activeCompanies = await _companyRepository.GetAll().ConfigureAwait(false);

            foreach (var company in activeCompanies)
            {
                var processingCommand = new AttendanceProcessDataCommand
                {
                    CompanyId = company.Id,
                    ProcessingStartDate = request.ProcessingStartDate,
                    ProcessingEndDate = request.ProcessingEndDate
                };
                await _mediator.Send(processingCommand);
            }

            return result;
        }
    }
}
