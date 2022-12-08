using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Interfaces
{
    public interface IReferenceRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
