export class EmployeeStatus {
  id: any;
  statusId: any;
  companyId: any;
  employeeStatusName: string = "";
  employeeStatusNameLC: string = "";
  rank: any;
  public constructor(init?: Partial<EmployeeStatus>) {
    Object.assign(this, init);
  }
}
