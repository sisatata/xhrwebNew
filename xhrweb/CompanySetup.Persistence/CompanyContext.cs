using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Persistence
{
    public class CompanyContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserContext _userContext;
        public CompanyContext(DbContextOptions<CompanyContext> options, IDomainEventDispatcher dispatcher,
            ICurrentUserContext userContext)
            : base(options)
        {
            _dispatcher = dispatcher;
            _userContext = userContext;
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<CommonLookUpType> CommonLookUpTypes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Upazila> Upazilas { get; set; }
        public DbSet<MonthCycle> MonthCycles { get; set; }
        public DbSet<OfficeNotice> OfficeNotices { get; set; }
        public DbSet<ConfirmationRule> ConfirmationRules { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SalaryRule> SalaryRules { get; set; }
        public DbSet<SalaryRuleDetail> SalaryRuleDetails { get; set; }
        public DbSet<DefaultConfiguration> DefaultConfigurations { get; set; }
        public DbSet<CustomConfiguration> CustomConfigurations { get; set; }

        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<CompanyEmail> CompanyEmails { get; set; }
        public DbSet<CompanyPhone> CompanyPhones { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<ActivityLogType> ActivityLogTypes { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
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
            modelBuilder.HasDefaultSchema("main");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyContext).Assembly);
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
