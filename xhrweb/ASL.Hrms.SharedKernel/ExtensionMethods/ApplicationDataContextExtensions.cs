using ASL.Hrms.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.ExtensionMethods
{
    public static class ApplicationDataContextExtensions
    {
        public static async Task<PagedResult<T, U>> GetPagedAsync<T, U>(this IQueryable<T> query, int page, int pageSize) where U : class
        {
            var result = new PagedResult<T, U>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.data = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public static PagedResult<T, U> GetPaged<T, U>(this IQueryable<T> query, int page, int pageSize, bool getAll = false) where U : class
        {
            var result = new PagedResult<T, U>
            {
                CurrentPage = getAll == false ? page : 1,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            if (getAll)
            {
                result.data = query.ToList();
            }

            else
            {
                var skip = (page - 1) * pageSize;
                result.data = query.Skip(skip).Take(pageSize).ToList();
            }
            return result;
        }

        public static async Task<PagedResult<T, U>> GetPagedAsync<T, U>(this IQueryable<T> query, int page, int pageSize, bool getAll = false) where U : class
        {
            var result = new PagedResult<T, U>
            {
                CurrentPage = getAll == false ? page : 1,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            if (getAll)
            {
                result.data = await query.ToListAsync();
            }

            else
            {
                var skip = (page - 1) * pageSize;
                result.data = await query.Skip(skip).Take(pageSize).ToListAsync();
            }
            return result;
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, bool getAll = false) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = getAll == false ? page : 1,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            if (getAll)
            {
                result.data = await query.ToListAsync();
            }

            else
            {
                var skip = (page - 1) * pageSize;
                result.data = await query.Skip(skip).Take(pageSize).ToListAsync();
            }

            return result;
        }

        public static PagedResult<T> GetPagedList<T>(this IQueryable<T> query, int page, int pageSize, bool getAll = false) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = getAll == false ? page : 1,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            if (getAll)
            {
                result.data = query.ToList();
            }

            else
            {
                var skip = (page - 1) * pageSize;
                result.data = query.Skip(skip).Take(pageSize).ToList();
            }

            return result;
        }

        // need to understand the below 2 extension method. For the time being just copy paste
        public static void UpdateManyToMany<T, TKey>(this DbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, TKey> getKey) where T : class
        {
            db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
            db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
        }


        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }

    }
}
