export class EmployeeEnrollment {
  id: any;
  employeeId: any;
  joiningDate?: any;
  quitDate?: any ;
  statusId: any;
  employeeStatusName: any;
  permanentDate?: any;
  jobTypeId: any;
  gradeId: any;
  subGradeId: any;
  employeeTypeId?: any;
  employeeCategoryId?: any;
  confirmationId?: any;
  genderId?: any;
  employeeImageUri: any;
  signatureUri:string='';
  leaveTypeGroup:string ='';
  leaveTypeGroupId:any ;
  leaveTypeGroupName:string='';
  public constructor(init?: Partial<EmployeeEnrollment>) {
    Object.assign(this, init);
  }
}
