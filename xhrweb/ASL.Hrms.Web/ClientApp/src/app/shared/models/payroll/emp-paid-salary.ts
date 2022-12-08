

export class EmployeePaidSalary {
  id: any;
  employeeId: any;
  branchId: any;
  departmentId: any;
  monthCycleId:any;
  fullName:any;
  salaryStructureId: any;
  financialYearId: any;
  grossSalary: any;
  companyId: any;
  departmentName: any;
  designationName: any;
  gradeName: any;
  payableSalary:any;
  totalDeductionAmount:any;
  netPayableAmount:any;
  branchIds: any;
  departmentIds: any;
  designationIds: any;
  positionName: string = "";
  companyEmployeeId: string = "";
  searchText: string = "";
  employeeSalaryProcessedDataComponentList: any[];
  public constructor(init?: Partial<EmployeePaidSalary>) {
    Object.assign(this, init);
  }
}



