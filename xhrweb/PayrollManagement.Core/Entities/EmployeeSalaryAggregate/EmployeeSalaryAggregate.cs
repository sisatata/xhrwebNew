using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using PayrollManagement.Core.ExtensionMethods;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Core.Entities.EmployeeSalaryAggregate
{
    public class EmployeeSalaryAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private readonly List<EmployeeSalary> employeeSalaries;
        private readonly List<SalaryStructureComponent> salaryStructureComponents;
        public readonly List<EmployeeSalaryComponent> employeeSalaryComponents;

        public EmployeeSalary EmployeeSalary { get; set; }
        public List<EmployeeSalaryComponent> EmployeeSalaryComponentList { get; set; }
        public EmployeeSalaryAggregate(List<EmployeeSalary> employeeSalaries,
            List<SalaryStructureComponent> salaryStructureComponents,
            List<EmployeeSalaryComponent> employeeSalaryComponents)
        {
            this.employeeSalaries = employeeSalaries;
            this.salaryStructureComponents = salaryStructureComponents;
            this.employeeSalaryComponents = employeeSalaryComponents;
            EmployeeSalary = new EmployeeSalary(Guid.NewGuid());
            EmployeeSalaryComponentList = new List<EmployeeSalaryComponent>();
        }
        public EmployeeSalaryAggregate(EmployeeSalary employeeSalary, List<EmployeeSalaryComponent> employeeSalaryComponentList)
        {
            EmployeeSalary = employeeSalary;
            EmployeeSalaryComponentList = employeeSalaryComponentList;
        }

        public void CreateEmployeeSalary(
         Guid? employeeId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         Guid? gradeId,
         Guid? salaryStructureId,
         Int16? paymentOptionId,
         decimal? grossSalary,
         Guid companyId,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted
        )

        {
            Guard.Against.NullOrEmptyGuid(companyId, "CompanyId");
            Guard.Against.DuplicateRecords(employeeSalaries, startDate.Value, endDate.Value, "");
            EmployeeSalary.Create(
                    employeeId,
                    branchId,
                    departmentId,
                    positionId,
                    gradeId,
                    salaryStructureId,
                    paymentOptionId,
                    grossSalary,
                    companyId,
                    startDate,
                    endDate,
                    isDeleted
            );
            //if (employeeSalaryComponents.Any())
            Guard.Against.InSufficientGross(grossSalary.Value, 0, "Gross", "Insufficient Amount");
            var fixedSalaryComponentList = salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Fixed).ToString() && x.IsDeleted == false);
            var remainingAmount = grossSalary - fixedSalaryComponentList.Sum(x => x.Value);
            Guard.Against.InSufficientGross(remainingAmount.Value, 0, "Gross", "Insufficient Amount");

            if (fixedSalaryComponentList != null && fixedSalaryComponentList.Count > 0)
            {
                foreach (var fixedComponent in fixedSalaryComponentList)
                {
                    //var empSalComp = new EmployeeSalaryComponent();
                    var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                             fixedComponent.Id,
                             fixedComponent.Value,
                             fixedComponent.CompanyId,
                             false
                    );
                    EmployeeSalaryComponentList.Add(entity);
                }
            }
            var percentSalaryComponentList = salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Percent).ToString() && x.IsDeleted == false);
            if (percentSalaryComponentList != null && percentSalaryComponentList.Count > 0)
            {
                foreach (var percentComponent in percentSalaryComponentList)
                {
                    //var empSalComp = new EmployeeSalaryComponent();
                    var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                             percentComponent.Id,
                             remainingAmount * (percentComponent.Value / 100),
                             percentComponent.CompanyId,
                             false
                    );
                    EmployeeSalaryComponentList.Add(entity);
                }
            }
        }
        public void StartEmployeeSalaryUpdate(
         Guid? employeeId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         Guid? gradeId,
         Guid? salaryStructureId,
         Int16? paymentOptionId,
         decimal? grossSalary,
         Guid companyId,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted
            )
        {
            //Guard.Against.NullOrEmptyGuid(Id, "Id");

            Guard.Against.NullOrEmptyGuid(companyId, "CompanyId");
            Guard.Against.DuplicateRecordInUpdate(employeeSalaries, EmployeeSalary.Id, startDate.Value, endDate.Value, "");
            Guard.Against.InSufficientGross(grossSalary.Value, 0, "Gross", "Insufficient Amount");
            var fixedSalaryComponentList = salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Fixed).ToString() && x.IsDeleted == false);
            var remainingAmount = grossSalary - fixedSalaryComponentList.Sum(x => x.Value);
            Guard.Against.InSufficientGross(remainingAmount.Value, 0, "Gross", "Insufficient Amount");

            EmployeeSalary.UpdateEmployeeSalary(
                employeeId,
                    branchId,
                    departmentId,
                    positionId,
                    gradeId,
                    salaryStructureId,
                    paymentOptionId,
                    grossSalary,
                    companyId,
                    startDate,
                    endDate,
                    isDeleted
                );

            if (fixedSalaryComponentList != null && fixedSalaryComponentList.Count > 0)
            {
                foreach (var fixedComponent in fixedSalaryComponentList)
                {
                    //var empSalComp = new EmployeeSalaryComponent();
                    var eComp = employeeSalaryComponents.FirstOrDefault(x => x.SalaryStructureComponentId == fixedComponent.Id);
                    if (eComp != null)
                    {
                        eComp.UpdateEmployeeSalaryComponent(EmployeeSalary.Id, fixedComponent.Id, fixedComponent.Value,
                            companyId, false);
                    }
                    else
                    {
                        //var empSalComp = new EmployeeSalaryComponent();
                        var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                                 fixedComponent.Id,
                                 fixedComponent.Value,
                                 fixedComponent.CompanyId,
                                 false
                        );
                        EmployeeSalaryComponentList.Add(entity);
                    }
                }
            }
            var percentSalaryComponentList = salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Percent).ToString() && x.IsDeleted == false);
            if (percentSalaryComponentList != null && percentSalaryComponentList.Count > 0)
            {
                foreach (var percentComponent in percentSalaryComponentList)
                {
                    var eComp = employeeSalaryComponents.FirstOrDefault(x => x.SalaryStructureComponentId == percentComponent.Id);
                    if (eComp != null)
                    {
                        eComp.UpdateEmployeeSalaryComponent(EmployeeSalary.Id, percentComponent.Id, remainingAmount * (percentComponent.Value / 100),
                            companyId, false);
                    }
                    else
                    {
                        var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                             percentComponent.Id,
                             remainingAmount * (percentComponent.Value / 100),
                             percentComponent.CompanyId,
                             false
                    );
                        EmployeeSalaryComponentList.Add(entity);
                    }
                }
            }
        }

    }
}
