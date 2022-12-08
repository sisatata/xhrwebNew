using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Entities;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Persistence
{
    public class AttendanceContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserContext _userContext;
        public AttendanceContext(DbContextOptions<AttendanceContext> options, IDomainEventDispatcher dispatcher,
            ICurrentUserContext userContext) : base(options)
        {
            _dispatcher = dispatcher;
            _userContext = userContext;
        }

        public DbSet<ShiftGroup> ShiftGroups { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftAllocation> ShiftAllocations { get; set; }

        public DbSet<AttendanceRequestApproveQueue> AttendanceRequestApproveQueues { get; set; }

        public DbSet<Attendance1> Attendance1 { get; set; }
        public DbSet<Attendance2> Attendance2 { get; set; }
        public DbSet<Attendance3> Attendance3 { get; set; }
        public DbSet<Attendance4> Attendance4 { get; set; }
        public DbSet<Attendance5> Attendance5 { get; set; }
        public DbSet<Attendance6> Attendance6 { get; set; }
        public DbSet<Attendance7> Attendance7 { get; set; }
        public DbSet<Attendance8> Attendance8 { get; set; }
        public DbSet<Attendance9> Attendance9 { get; set; }
        public DbSet<Attendance10> Attendance10 { get; set; }
        public DbSet<Attendance11> Attendance11 { get; set; }
        public DbSet<Attendance12> Attendance12 { get; set; }
        public DbSet<AttendanceRequest> AttendanceRequests { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
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

            modelBuilder.HasDefaultSchema("attendance");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AttendanceContext).Assembly);
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
