using Microsoft.EntityFrameworkCore;
using EmployeeEnrollment.Core.Entities;
using System.Linq;
using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;
using System.Threading;
using ASL.Hrms.SharedKernel;

namespace EmployeeEnrollment.Persistence
{
    public class EmployeeContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserContext _userContext;
        public EmployeeContext(DbContextOptions<EmployeeContext> options, IDomainEventDispatcher dispatcher,
            ICurrentUserContext userContext)
            : base(options)
        {
            _dispatcher = dispatcher;
            _userContext = userContext;
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<EmployeeEmail> EmployeeEmails { get; set; }
        public DbSet<EmployeeEnrollment.Core.Entities.EmployeeEnrollment> EmployeeEnrollments { get; set; }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
        public DbSet<EmployeePhone> EmployeePhones { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
        public DbSet<EmployeeStatusHistory> EmployeeStatusHistories { get; set; }
        public DbSet<EmployeeEducation> EmployeeEducations { get; set; }
        public DbSet<EmployeeExperience> EmployeeExperiences { get; set; }
        public DbSet<EmployeeFamilyMember> EmployeeFamilyMembers { get; set; }
        public DbSet<EmployeeManager> EmployeeManagers { get; set; }
        public DbSet<EmployeeCard> EmployeeCards { get; set; }
        public DbSet<EmployeeDevice> EmployeeDevices { get; set; }
        public DbSet<EmployeeCustomConfiguration> EmployeeCustomConfigurations { get; set; }
        public DbSet<EmployeePromotionTransfer> EmployeePromotionTransfers { get; set; }

        public DbSet<EmployeeNote> EmployeeNotes { get; set; }
        public DbSet<PreviousPFGratuityEarnLeave> PreviousPFGratuityEarnLeaves { get; set; }

        public DbSet<EmployeeRawDataHist> EmployeeRawDataHists { get; set; }
        public DbSet<EmployeeRawDataPrep> EmployeeRawDataPreps { get; set; }

        public DbSet<RawFileDetail> RawFileDetails { get; set; }
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

            modelBuilder.HasDefaultSchema("employee");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);
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
