using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using EmployeeNote = EmployeeEnrollment.Application.EmployeeNote.Commands;
using CompanySetup.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CompanySetup.Core.Entities;


namespace Asl.Infrastructure
{
    public class EmployeeNoteService : IEmployeeNoteService
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserContext _userContext;
        public EmployeeNoteService(IMediator mediator, ICurrentUserContext currentUserContext)
        {
            _mediator = mediator;
            _userContext = currentUserContext;
        }
        public async Task<bool> InsertEmployeeNote(Guid employeeId,
            string note,
            Guid downloadId,
            Boolean displayToEmployee)
        {
            var employeeNoteCommand = new EmployeeNote.Commands.V1.CreateEmployeeNote
            {
                EmployeeId = employeeId,
                Note = note,
                DownloadId = downloadId,
                DisplayToEmployee = displayToEmployee
            };
            await _mediator.Send(employeeNoteCommand).ConfigureAwait(false);
            return true;
        }
    }
}
