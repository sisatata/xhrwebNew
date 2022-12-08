export class AttendanceProcessData {
  punchDate: any;
  timeIn: any;
  timeOut: any;
  status: any;
  shiftIn: any;
  shiftOut: any;
  employeeRemarks: string = "";
  late: any;
  clockInAddress: string = "";
  clockOutAddress: string = "";
  shiftCode: string = "";
  employeeName:string="";
  branchId: any;
  branchName: string = "";
  departmentId: any;
  departmentName: string = "";
  positionId: any;
  positionName: string = "";
  employeeId: string ="";
  public constructor(init?: Partial<AttendanceProcessData>) {
    Object.assign(this, init);
  }

}
