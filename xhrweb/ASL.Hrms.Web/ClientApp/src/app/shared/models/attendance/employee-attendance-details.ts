export class EmployeeAttendanceDetails {
    id:any;
    punchDate: any;
    punchtype:number;
    latitude:number;
    longitude:number;
    clockTimeAddress:string='';
    clockTimeTypeText:string = '';
    clockTimeType:string='';
    attendanceDate:any;
    fullName:string = '';
    punchtypeText:string='';
    positionId:any;
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
    positionName: string = "";
    employeeId: any;
    public constructor(init?: Partial<EmployeeAttendanceDetails>) {
      Object.assign(this, init);
    }
  
  }
  