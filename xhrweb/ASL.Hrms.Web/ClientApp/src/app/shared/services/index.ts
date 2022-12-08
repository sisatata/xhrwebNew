// Don't import base-service or any base class
export * from './file-upload.service';
export {DataService} from './data.service';
//Attendance Module
export { ShiftGroupService } from './attendance/shift-group.service';
export { ShiftService } from './attendance/shift.service';
export { ShiftAllocationService } from './attendance/shift-allocation.service';
export { HolidayService } from './attendance/holiday.service';
export { AttendanceService } from './attendance/attendance.service';

//Company Module
export { CompanyService } from './company/company.service';
export { BranchService } from './company/branch.service';
export { DepartmentService } from './company/department.service';
export { DesignationService } from './company/designation.service';
export { FinancialYearService } from './company/financial-year.service';
export { CommonLookUpTypeService } from './company/common-lookup-type.service';
export { MonthCycleService } from './company/month-cycle.service';
export { OfficeNoticeService } from './company/office-notice.service';
export { ConfigurationService } from './company/configuration.service';  
export { CustomConfigurationService } from './company/custom-configuration.service';
export { CompanyEmailService } from './company/company-email-service';
export { CompanyPhoneService } from './company/companyphone.service';
export { CompanyAddressService } from './company/companyaddress.service';
//CustomEmployeeConfigurationService
export { CustomEmployeeConfigurationService } from './company/employee-custom-configuration.service';


//Employee Module
export { EmployeeService } from './employee/employee.service';
export { EmployeeDetailService } from './employee/employee-detail.service';
export { EmployeeAddressService } from './employee/employee-address.service';
export { EmployeeEmailService } from './employee/employee-email.service';
export { EmployeePhoneService } from './employee/employee-phone.service';
export { EmployeeExperienceService } from './employee/employee-experience.service';
export { EmployeeEducationService } from './employee/employee-education.service';
export { EmployeeFamilyMemberService } from './employee/employee-family-member.service';
// EmployeeCardService EmployeePromotionTransferService CommonSettingsService
export { EmployeeManagerService } from './employee/employee-manager.service';
export { EmployeeEnrollmentService } from './employee/employee-enrollment.service';
export { EmployeeStatusService } from './employee/employee-status.service';
export { EmployeeStatusHistoryService } from './employee/employee-status-history.service';
export { EmployeeCardService } from './employee/employee-card.service';
export { EmployeePromotionTransferService } from './employee/employee-promotion-transfer.service';

export { CommonSettingsService } from './common/common-settings.service';
//Leave Module LeaveGroupService
export { LeaveTypeService } from './leave/leave-type.service';
export { LeaveApplicationService } from './leave/leave-application.service';
export { LeaveGroupService } from './leave/leave-group.service';
//Payroll Module & Report module EarnLeaveEncashmentService EarnLeaveEncashmentReportService ProvidentFundReportService
export { BankService } from './payroll/bank.service';
export { BankBranchService } from './payroll/bank-branch.service';
export { EmployeeBankAccountService } from './payroll/employee-bank-account.service';
export { PaymentOptionService } from './payroll/payment-option.service';
export { SalaryStructureService } from './payroll/salary-structure.service';
export { SalaryStructureComponentService } from './payroll/salary-structure-component.service';
export { EarnLeaveEncashmentService } from './payroll/earn-leave-encashment.service';
export { EmployeeSalaryService } from './payroll/employee-salary.service';
export { EmployeeSalaryCompService } from './payroll/employee-salary-comp.service';
export { EmployeeSalaryProcessService } from './payroll/employee-salary-process.service';
export { BenefitDeductionService  } from './payroll/benefit-deduction.service';
export { BenefitDeductionConfigService  } from './payroll/benefit-deduction-config.service';
export { BenefitDeductionIntervalService  } from './payroll/benefit-deduction-interval.service';
export { BenefitDeductionEmployeeService  } from './payroll/benefit-deduction-employee.service';
export { IncomeTaxPayerTypeService  } from './payroll/income-tax-payer-type.service';
export { IncomeTaxSlabService  } from './payroll/income-tax-slab.service';
export { IncomeTaxParameterService  } from './payroll/income-tax-parameter.service'; 
export { BenefitBillClaimService  } from './payroll/bill-claim.service';
export { IncomeTaxInvestmentService  } from './payroll/income-tax-investment.service';
export { TaskCategoryService  } from './task/task-category.service';
export { TaskDetailService  } from './task/task-detail.service';
export { TaskDetailLogService  } from './task/task-detail-log.service';
export { DistrictsService  } from './common/district.service'; 
export { AttendanceReportService  } from './report/attendance-report.service'; 
export { BillClaimReportService  } from './report/bill-claim-report.service'; 
export { SalaryReportService  } from './report/salary-report.service'; 
export { LeaveReportService  } from './report/leave-report.service'; 
export { EarnLeaveEncashmentReportService  } from './report/earn-leave-encashment-reprot.service';
export { RoleManagementDataService  } from './admin/role-management.service';
export { ProvidentFundReportService  } from './report/provident-fund-report.service';
// AttendanceRequestService   FileService TaskReportService 
export { AttendanceChartService  } from './chart/attendance-chart.service';
export { AttendanceRequestService  } from './attendance/attendance-request.service';
export { PayrollChartService  } from './chart/payroll-chart.service';
export { CountChartService  } from './chart/count-chart.service';
export { BonsuConfigurationService  } from './payroll/festival-bonus.service';
export { BonusReportService  } from './report/bonus-report.service';
export { EmployeeEnrollReportService  } from './report/employee-enroll-report.service';
export { OTSummaryReportService  } from './report/ot-summary-report.service';
export { TaskReportService  } from './report/task-report.service';
export { FileService  } from './common/file.service';