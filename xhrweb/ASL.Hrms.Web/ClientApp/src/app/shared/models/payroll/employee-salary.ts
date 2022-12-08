import { EmployeeSalaryComp } from "./employee-salary-comp";

export class EmployeeSalary {
  id: any;
  employeeId: any;
  branchId: any;
  departmentId: any;
  positionId: any;
  gradeId: any;
  salaryStructureId: any;
  paymentOptionId: any;
  grossSalary: any;
  companyId: any;
  startDate: any;
  endDate: any;
  branchName: any;
  departmentName: any;
  designationName: any;
  gradeName: any;
  structureName: any;
  optionName: any;
  fullName: any;
  branchIds: any;
  departmentIds: any;
  designationIds: any;
  positionName: string = "";
  companyEmployeeId: string = "";
  searchText: string = "";
  employeeSalaryComponentList: any[];
  public constructor(init?: Partial<EmployeeSalary>) {
    Object.assign(this, init);
  }
}
