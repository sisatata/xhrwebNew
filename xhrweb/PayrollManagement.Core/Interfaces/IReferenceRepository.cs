using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManagement.Core.Interfaces
{
    public interface IReferenceRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}