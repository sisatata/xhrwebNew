export class AttendanceReport {
  id: any;
  branchIds: any;
  departmentIds: any;
  designationIds: any;
  employeeIds: any;
  companyId: any;
  startTime: any;
  endTime: any;

  public constructor(init?: Partial<AttendanceReport>) {
    Object.assign(this, init);
  }
}
