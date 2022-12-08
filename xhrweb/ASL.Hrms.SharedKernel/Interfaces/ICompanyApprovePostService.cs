using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface ICompanyApprovePostService
    {
        Task<bool> AddDefaultEmployeeStatuses(Guid? CompanyId);
        Task<bool> AddDefaultGrades(Guid? CompanyId);
        Task<bool> AddDefaultEmployeeConfirmationRules(Guid? CompanyId);

        Task AddDefaultValues(Guid? CompanyId);
    }
}
