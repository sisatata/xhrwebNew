using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Persistence
{
   public class TaskMngtContext : DbContext
    {
        public TaskMngtContext(DbContextOptions<TaskMngtContext> options, IDomainEventDispatcher dispatcher,
            ICurrentUserContext userContext)
            : base(options)
        {
            _dispatcher = dispatcher;
            _userContext = userContext;
        }

        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserContext _userContext;
        public DbSet<TaskCategory> TaskCategories { get; set; }
        public DbSet<TaskDetail> TaskDetails { get; set; }
        public DbSet<TaskDetailLog> TaskDetailLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                                               .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType));

            foreach (var entity in entityTypes)
            {
                modelBuilder.Entity(entity.ClrType).Property<string>("CreatedBy").HasMaxLength(250);
                modelBuilder.Entity(entity.ClrType).Property<DateTime?>("CreatedDate");
                modelBuilder.Entity(entity.ClrType).Property<string>("ModifiedBy").HasMaxLength(250);
                modelBuilder.Entity(entity.ClrType).Property<DateTime?>("ModifiedDate");
            }

            modelBuilder.HasDefaultSchema("task");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskMngtContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditInformation();
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<Guid>>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await _dispatcher.Dispatch(domainEvent).ConfigureAwait(false);
                }
            }

            return result;
        }

        public override int SaveChanges()
        {
            UpdateAuditInformation();
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
        private void UpdateAuditInformation()
        {
            var modifiedEntities = ChangeTracker.Entries<IAuditable>()
                                        .Where(c => c.State == EntityState.Added || c.State == EntityState.Modified);

            foreach (var entity in modifiedEntities)
            {
                entity.Property("ModifiedDate").CurrentValue = DateTime.Now;
                entity.Property("ModifiedBy").CurrentValue = _userContext.CurrentUserId;

                if (entity.State == EntityState.Added)
                {
                    entity.Property("CreatedDate").CurrentValue = DateTime.Now;
                    entity.Property("CreatedBy").CurrentValue = _userContext.CurrentUserId;
                }
            }
        }
    }
}
