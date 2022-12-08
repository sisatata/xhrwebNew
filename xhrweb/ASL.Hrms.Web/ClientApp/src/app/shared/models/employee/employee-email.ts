export class EmployeeEmail{
  id: any;
  employeeId: any;
  emailAddress: string = "";
  isPrimary: boolean=false;

  public constructor(init?: Partial<EmployeeEmail>) {
    Object.assign(this, init);
  }
}
