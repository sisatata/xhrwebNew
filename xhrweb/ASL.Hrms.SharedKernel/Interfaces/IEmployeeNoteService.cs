using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IEmployeeNoteService
    {
        Task<bool> InsertEmployeeNote(
            Guid employeeId,
            string note,
            Guid downloadId,
            Boolean displayToEmployee);
    }
}
