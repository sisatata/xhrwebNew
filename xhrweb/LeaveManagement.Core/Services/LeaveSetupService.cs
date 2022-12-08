using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Services
{
    public class LeaveSetupService : ILeaveSetupService
    {
        public LeaveSetupService(IReferenceRepository<Company, Guid> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        private readonly IReferenceRepository<Company, Guid> _companyRepository;
        public async Task<bool> CheckCompanyStatus(Guid companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            return true;
        }
    }
}
