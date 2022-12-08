using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IEmployeeSalaryService
    {
        Task<bool> AddNewEmployeeSalary(Guid? EmployeeId, Guid? BranchId,
            Guid? DepartmentId, Guid? PositionId,
            Guid? GradeId, Guid? SalaryStructureId,
            Int16? PaymentOptionId, decimal? GrossSalary,
            Guid? CompanyId,
            DateTime? StartDate, DateTime? EndDate,
            Boolean IsDeleted,
            Boolean isRequestFromPromotion = true);
    }
}
