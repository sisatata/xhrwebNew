using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Interfaces
{
    public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>//, IAggregateRoot
    {
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
