using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.ExtensionMethods;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeEnrollment.Core.Entities.EmployeePromotionTransferAggregate
{
    public class EmployeePromotionTransferAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private readonly List<EmployeePromotionTransfer> _employeePromotionTransfers;
        private readonly List<EmployeeSalary> _employeeSalaries;
        private readonly List<EmployeeSalaryComponent> _employeeSalaryComponents;
        private readonly List<SalaryStructureComponent> _salaryStructureComponents;
        private readonly int yearsToBeAdded = 2;

        public EmployeePromotionTransfer EmployeePromotionTransfer { get; set; }
        public EmployeePromotionTransferAggregate(List<EmployeePromotionTransfer> employeePromotionTransfers,
            List<EmployeeSalary> employeeSalaries,
            List<EmployeeSalaryComponent> employeeSalaryComponents,
            List<SalaryStructureComponent> salaryStructureComponents
            )
        {
            _employeePromotionTransfers = employeePromotionTransfers;
            _employeeSalaries = employeeSalaries;
            _employeeSalaryComponents = employeeSalaryComponents;
            _salaryStructureComponents = salaryStructureComponents;
            EmployeePromotionTransfer = new EmployeePromotionTransfer(Guid.NewGuid());
        }

        async public void PrepareEmployeePromotionTransfer(

            Guid? employeeId,
            Guid? previousCompanyId,
            Guid? previousBranchId,
            Guid? previousDepartmentId,
            Guid? previousPositionId,
            Guid? newCompanyId,
            Guid? newBranchId,
            Guid? newDepartmentId,
            Guid? newPositionId,
            DateTime? proposedDate,
            DateTime startDate,
            DateTime? endDate,
            decimal? previousGross,
            decimal? newGross,
            decimal? previousBasic,
            decimal? newBasic,
            decimal? previousHouserent,
            decimal? newHouserent,
            Int32? incrementTypeId,
            Int32? incrementValueTypeId,
            decimal? incrementValue,
            decimal? incrementAmount,
            Int32? incrementOn,
            string reason,
            string reference,

            Guid? previousGradeId,
            Guid? newGradeId,
            Guid? previousSalaryStructureId,
            Guid? newSalaryStructureId,
            short? previousPaymentOptionId,
            short? newPaymentOptionId
           )

        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            if (endDate == null)
            {
                endDate = startDate.AddYears(yearsToBeAdded);
            }
            Guard.Against.DuplicateRecords(_employeePromotionTransfers, startDate, endDate.Value, "");

            decimal? _grossSalary = 0.0M;
            decimal _payableBasic = 0;
            decimal _payableHouseRent = 0;

            var employeeSalary = _employeeSalaries.FirstOrDefault(es => es.EmployeeId == employeeId);
            if (employeeSalary != null)
            {
                previousCompanyId = employeeSalary.CompanyId;
                previousBranchId = employeeSalary.BranchId;
                previousDepartmentId = employeeSalary.DepartmentId;
                previousPositionId = employeeSalary.PositionId;
            }
            _grossSalary = employeeSalary.GrossSalary;

            var _empSalaryComponentList = _employeeSalaryComponents.FindAll(x => x.EmployeeSalaryId == employeeSalary.Id);


            //Basic

            var basicComponent = from p in _empSalaryComponentList
                                 join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                 where sc.SalaryComponentName.ToUpper() == "BASIC"
                                 select p;


            if (basicComponent.Any())
            {
                var _basicComponent = basicComponent.FirstOrDefault();
                _payableBasic = _basicComponent.ComponentAmount.Value;
                previousBasic = _payableBasic;
            }
            //HouseRent 
            var houseRentComponent = from p in _empSalaryComponentList
                                     join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                     where sc.SalaryComponentName.ToUpper() == "HOUSE RENT"
                                     select p;

            if (houseRentComponent.Any())
            {
                var _houseRentComponent = houseRentComponent.FirstOrDefault();
                _payableHouseRent = _houseRentComponent.ComponentAmount.Value;
                previousHouserent = _payableHouseRent;
            }

            if (newGradeId == null || newGradeId == Guid.Empty)
            {
                newGradeId = previousGradeId;
            }
            if (newSalaryStructureId == null || newSalaryStructureId == Guid.Empty)
            {
                newSalaryStructureId = previousSalaryStructureId;
            }
            if (newPaymentOptionId == null || newPaymentOptionId < 1)
            {
                newPaymentOptionId = previousPaymentOptionId;
            }

            switch (incrementTypeId)
            {
                case 10:
                case 20:
                case 30:
                case 50:
                case 70:
                case 80:
                    if (incrementValueTypeId == (int)ValueTypes.Percent)
                    {
                        decimal? percent = 0.0M;
                        percent = (incrementValue / 100);
                        switch (incrementOn)
                        {
                            case (int)IncreametOnTypes.Gross:
                                incrementAmount = _grossSalary * percent;
                                break;
                            case (int)IncreametOnTypes.Basic:
                                incrementAmount = _payableBasic * percent;
                                break;
                            case (int)IncreametOnTypes.BasicAndHouserent:
                                incrementAmount = (_payableBasic + _payableHouseRent) * percent;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        incrementAmount = incrementValue;
                    newGross = previousGross + incrementAmount;
                    #region -- salary distribute
                    var fixedSalaryComponentList = _salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Fixed).ToString() && x.IsDeleted == false);
                    var remainingAmount = newGross - fixedSalaryComponentList.Sum(x => x.Value);

                    //if (fixedSalaryComponentList != null && fixedSalaryComponentList.Count > 0)
                    //{
                    //    foreach (var fixedComponent in fixedSalaryComponentList)
                    //    {
                    //        //var empSalComp = new EmployeeSalaryComponent();
                    //        var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                    //                 fixedComponent.Id,
                    //                 fixedComponent.Value,
                    //                 fixedComponent.CompanyId,
                    //                 false
                    //        );
                    //        EmployeeSalaryComponentList.Add(entity);
                    //    }
                    //}
                    var percentSalaryComponentList = _salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Percent).ToString() && x.IsDeleted == false);
                    if (percentSalaryComponentList != null && percentSalaryComponentList.Count > 0)
                    {
                        foreach (var percentComponent in percentSalaryComponentList)
                        {
                            //var empSalComp = new EmployeeSalaryComponent();
                            //var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                            //         percentComponent.Id,
                            //         remainingAmount * (percentComponent.Value / 100),
                            //         percentComponent.CompanyId,
                            //         false
                            //);
                            //EmployeeSalaryComponentList.Add(entity);

                            if (percentComponent.SalaryComponentName.ToUpper() == "BASIC")
                            {
                                newBasic = remainingAmount * (percentComponent.Value / 100);
                            }
                            if (percentComponent.SalaryComponentName.ToUpper() == "HOUSE RENT")
                            {
                                newHouserent = remainingAmount * (percentComponent.Value / 100);
                            }
                        }
                    }
                    #endregion
                    break;
                case 40:
                case 60:
                    newGross = previousGross;
                    newBasic = previousBasic;
                    newHouserent = previousHouserent;
                    break;
                default:
                    break;
            }
            EmployeePromotionTransfer.Create(
            employeeId,
            previousCompanyId,
            previousBranchId,
            previousDepartmentId,
            previousPositionId,
            newCompanyId,
            newBranchId,
            newDepartmentId,
            newPositionId,
            proposedDate,
            startDate,
            endDate,
            previousGross,
            newGross,
            previousBasic,
            newBasic,
            previousHouserent,
            newHouserent,
            incrementTypeId,
            incrementValueTypeId,
            incrementValue,
            incrementAmount,
            incrementOn,
            reason,
            reference,
            previousGradeId,
            newGradeId,
            previousSalaryStructureId,
            newSalaryStructureId,
            previousPaymentOptionId,
            newPaymentOptionId
        );


        }

        async public void PrepareEmployeePromotionTransferUpdate(

            Guid? employeeId,
            Guid? previousCompanyId,
            Guid? previousBranchId,
            Guid? previousDepartmentId,
            Guid? previousPositionId,
            Guid? newCompanyId,
            Guid? newBranchId,
            Guid? newDepartmentId,
            Guid? newPositionId,
            DateTime? proposedDate,
            DateTime startDate,
            DateTime? endDate,
            decimal? previousGross,
            decimal? newGross,
            decimal? previousBasic,
            decimal? newBasic,
            decimal? previousHouserent,
            decimal? newHouserent,
            Int32? incrementTypeId,
            Int32? incrementValueTypeId,
            decimal? incrementValue,
            decimal? incrementAmount,
            Int32? incrementOn,
            string reason,
            string reference,
            Guid? previousGradeId,
            Guid? newGradeId,
            Guid? previousSalaryStructureId,
            Guid? newSalaryStructureId,
            short? previousPaymentOptionId,
            short? newPaymentOptionId
           )

        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            if (endDate == null)
            {
                endDate = startDate.AddYears(yearsToBeAdded);
            }
            Guard.Against.DuplicateRecordInUpdate(_employeePromotionTransfers, EmployeePromotionTransfer.Id, startDate, endDate.Value, "");

            decimal? _grossSalary = 0.0M;
            decimal _payableBasic = 0;
            decimal _payableHouseRent = 0;

            var employeeSalary = _employeeSalaries.FirstOrDefault(es => es.EmployeeId == employeeId);
            _grossSalary = employeeSalary.GrossSalary;

            var _empSalaryComponentList = _employeeSalaryComponents.FindAll(x => x.EmployeeSalaryId == employeeSalary.Id);


            //Basic

            var basicComponent = from p in _empSalaryComponentList
                                 join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                 where sc.SalaryComponentName.ToUpper() == "BASIC"
                                 select p;


            if (basicComponent.Any())
            {
                var _basicComponent = basicComponent.FirstOrDefault();
                _payableBasic = _basicComponent.ComponentAmount.Value;
            }
            //HouseRent 
            var houseRentComponent = from p in _empSalaryComponentList
                                     join sc in _salaryStructureComponents on p.SalaryStructureComponentId equals sc.Id
                                     where sc.SalaryComponentName.ToUpper() == "HOUSE RENT"
                                     select p;

            if (houseRentComponent.Any())
            {
                var _houseRentComponent = houseRentComponent.FirstOrDefault();
                _payableHouseRent = _houseRentComponent.ComponentAmount.Value;
            }

            if (newGradeId == null || newGradeId == Guid.Empty)
            {
                newGradeId = previousGradeId;
            }
            if (newSalaryStructureId == null || newSalaryStructureId == Guid.Empty)
            {
                newSalaryStructureId = previousSalaryStructureId;
            }
            if (newPaymentOptionId == null || newPaymentOptionId < 1)
            {
                newPaymentOptionId = previousPaymentOptionId;
            }
            switch (incrementTypeId)
            {
                case 10:
                case 20:
                case 30:
                case 50:
                case 70:
                case 80:
                    if (incrementValueTypeId == (int)ValueTypes.Percent)
                    {
                        decimal? percent = 0.0M;
                        percent = (incrementValue / 100);
                        switch (incrementOn)
                        {
                            case (int)IncreametOnTypes.Gross:
                                incrementAmount = _grossSalary * percent;
                                break;
                            case (int)IncreametOnTypes.Basic:
                                incrementAmount = _payableBasic * percent;
                                break;
                            case (int)IncreametOnTypes.BasicAndHouserent:
                                incrementAmount = (_payableBasic + _payableHouseRent) * percent;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        incrementAmount = incrementValue;
                    newGross = previousGross + incrementAmount;
                    #region -- salary distribute
                    var fixedSalaryComponentList = _salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Fixed).ToString() && x.IsDeleted == false);
                    var remainingAmount = newGross - fixedSalaryComponentList.Sum(x => x.Value);

                    //if (fixedSalaryComponentList != null && fixedSalaryComponentList.Count > 0)
                    //{
                    //    foreach (var fixedComponent in fixedSalaryComponentList)
                    //    {
                    //        //var empSalComp = new EmployeeSalaryComponent();
                    //        var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                    //                 fixedComponent.Id,
                    //                 fixedComponent.Value,
                    //                 fixedComponent.CompanyId,
                    //                 false
                    //        );
                    //        EmployeeSalaryComponentList.Add(entity);
                    //    }
                    //}
                    var percentSalaryComponentList = _salaryStructureComponents.FindAll(x => x.ValueType == ((int)ValueTypes.Percent).ToString() && x.IsDeleted == false);
                    if (percentSalaryComponentList != null && percentSalaryComponentList.Count > 0)
                    {
                        foreach (var percentComponent in percentSalaryComponentList)
                        {
                            //var empSalComp = new EmployeeSalaryComponent();
                            //var entity = PayrollEntities.EmployeeSalaryComponent.Create(

                            //         percentComponent.Id,
                            //         remainingAmount * (percentComponent.Value / 100),
                            //         percentComponent.CompanyId,
                            //         false
                            //);
                            //EmployeeSalaryComponentList.Add(entity);

                            if (percentComponent.SalaryComponentName.ToUpper() == "BASIC")
                            {
                                newBasic = remainingAmount * (percentComponent.Value / 100);
                            }
                            if (percentComponent.SalaryComponentName.ToUpper() == "HOUSERENT")
                            {
                                newHouserent = remainingAmount * (percentComponent.Value / 100);
                            }
                        }
                    }
                    #endregion
                    break;
                case 40:
                case 60:
                    newGross = previousGross;
                    newBasic = previousBasic;
                    newHouserent = previousHouserent;
                    break;
                default:
                    break;
            }

            EmployeePromotionTransfer.UpdateEmployeePromotionTransfer(
            employeeId,
            previousCompanyId,
            previousBranchId,
            previousDepartmentId,
            previousPositionId,
            newCompanyId,
            newBranchId,
            newDepartmentId,
            newPositionId,
            proposedDate,
            startDate,
            endDate,
            previousGross,
            newGross,
            previousBasic,
            newBasic,
            previousHouserent,
            newHouserent,
            incrementTypeId,
            incrementValue,
            incrementAmount,
            incrementOn,
            reason,
            reference,
            previousGradeId,
            newGradeId,
            previousSalaryStructureId,
            newSalaryStructureId,
            previousPaymentOptionId,
            newPaymentOptionId
        );


        }
    }
}
