using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using PayrollManagement.Core.Entities;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;
using PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Persistence
{
    public class PayrollContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserContext _userContext;
        public PayrollContext(DbContextOptions<PayrollContext> options, IDomainEventDispatcher dispatcher,
            ICurrentUserContext userContext)
            : base(options)
        {
            _dispatcher = dispatcher;
            _userContext = userContext;
        }

        public DbSet<BenefitDeductionCode> BenefitDeductionCodes { get; set; }
        public DbSet<BenefitDeductionConfig> BenefitDeductionConfigs { get; set; }
        public DbSet<BenefitDeductionEmployeeAssigned> BenefitDeductionEmployeeAssigneds { get; set; }
        public DbSet<BenefitDeductionInterval> BenefitDeductionIntervals { get; set; }
        public DbSet<BenefitBillClaim> BenefitBillClaims { get; set; }
        public DbSet<BenefitBillClaimApproveQueue> BenefitBillClaimApproveQueues { get; set; }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<EmployeeBankAccount> EmployeeBankAccounts { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<EmployeeSalaryComponent> EmployeeSalaryComponents { get; set; }
        public DbSet<SalaryStructure> SalaryStructures { get; set; }
        public DbSet<SalaryStructureComponent> SalaryStructureComponents { get; set; }
        public DbSet<PaymentOption> PaymentOptions { get; set; }
        public DbSet<EmployeeSalaryProcessedData> EmployeeSalaryProcessedDatas { get; set; }
        public DbSet<EmployeeSalaryProcessedDataComponent> EmployeeSalaryProcessedDataComponents { get; set; }

        public DbSet<IncomeTaxInvestment> IncomeTaxInvestments { get; set; }
        public DbSet<IncomeTaxParameter> IncomeTaxParameters { get; set; }
        public DbSet<IncomeTaxPayerType> IncomeTaxPayerTypes { get; set; }
        public DbSet<IncomeTaxSlab> IncomeTaxSlabs { get; set; }
        public DbSet<EmployeePaidIncomeTax> EmployeePaidIncomeTaxes { get; set; }

        public DbSet<BonusConfiguration> BonusConfigurations { get; set; }
        public DbSet<EmployeeBonusProcessedData> EmployeeBonusProcessedDatas { get; set; }

        public DbSet<EmployeeLeaveEncashment> EmployeeLeaveEncashments { get; set; }
        public DbSet<EmployeePFTransaction> EmployeePFTransactions { get; set; }
        
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

            modelBuilder.HasDefaultSchema("payroll");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PayrollContext).Assembly);
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
