export class EmployeeStatusHistory {
  id: any;
  employeeId: any;
  statusId: any;
  changedDate: any;
  remarks: string = "";
  statusName: string = "";
  public constructor(init?: Partial<EmployeeStatusHistory>) {
    Object.assign(this, init);
  }
}
