export class EmployeeDetail {
  id: any;
  employeeId: any;
  fathersName: string = "";
  mothersName: string = "";
  spouseName: string = "";
  maritalStatusId: any;
  religionId: any;
  nid: string = "";
  bid: string="";
  bloodGroupId?: any;
  public constructor(init?: Partial<EmployeeDetail>) {
    Object.assign(this, init);
  }
}
