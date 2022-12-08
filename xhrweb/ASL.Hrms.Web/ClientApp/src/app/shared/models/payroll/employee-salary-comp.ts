export class EmployeeSalaryComp {

  id: any;
  employeeSalaryId: any;
  salaryStructureComponentId: any;
  componentAmount: any;
  companyId: any;
  salaryComponentName: any;

  public constructor(init?: Partial<EmployeeSalaryComp>) {
    Object.assign(this, init);
  }
}



