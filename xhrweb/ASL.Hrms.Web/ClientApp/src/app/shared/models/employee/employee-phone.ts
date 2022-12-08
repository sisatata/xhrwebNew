export class EmployeePhone{
  id: any;
  employeeId: any;
  phoneNumber: string="";
  phoneTypeId: any;

  public constructor(init?: Partial<EmployeePhone>) {
    Object.assign(this, init);
  }
}
