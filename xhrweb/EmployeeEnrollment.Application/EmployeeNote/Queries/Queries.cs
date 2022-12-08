using EmployeeEnrollment.Application.EmployeeNote.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeNote.Queries
{
    public static class Queries
    {
        public class GetEmployeeNoteList : IRequest<List<EmployeeNoteVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetEmployeeNote : IRequest<EmployeeNoteVM>
        {
            public Guid Id { get; set; }
        }
    }
}
