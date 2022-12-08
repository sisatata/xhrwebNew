using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySetup.Persistence
{
   public class ReferenceRepository<T, TId> : IReferenceRepository<T, TId> where T : BaseEntity<TId>
    {
        public ReferenceRepository(ReferenceContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected readonly ReferenceContext _dataContext;

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dataContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dataContext.Set<T>().AsQueryable(), spec);
        }
    }
}
