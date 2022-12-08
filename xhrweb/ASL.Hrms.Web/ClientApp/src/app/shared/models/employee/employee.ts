export class Employee {
  id: any;
  companyId: any;
  branchId: any;
  branchName: string = "";
  departmentId: any;
  departmentName: string = "";
  positionId: any;
  positionName: string = "";
  employeeId: string ="";
  fullName: string = "";
  fullNameLC: string = "";
  dateOfBirth: any;
  referenceNumber: string = "";
  nationalityId?: any;
  nationalityName: string = "";
  genderId?: any;
  genderName: string = "";
  userId: string = "";
  password: string = "";
  gradeId: any;
  gradeName: any = "";
  
  public constructor(init?: Partial<Employee>) {
    Object.assign(this, init);
  }
}
