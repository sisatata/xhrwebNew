using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanySetup.Core.Interfaces
{
    public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>, IAggregateRoot
    {
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
