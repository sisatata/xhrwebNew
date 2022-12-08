using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Persistence
{
    public class EfRepository<T, TId> : IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly AttendanceContext _dataContext;

        public EfRepository(AttendanceContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dataContext.Set<T>().AsQueryable(), spec);
        }
    }
}
