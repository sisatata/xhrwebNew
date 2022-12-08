export class Attendance {
  id: any;
  employeeId: any;
  attendanceDate: any;
  punchtype: any;
  overNightMark: boolean = false;
  latitude: number;
  longitude: number;
  clockTimeType: any;
  remarks: string = "";
  clockTimeAddress: string = "";

  public constructor(init?: Partial<Attendance>) {
    Object.assign(this, init);
  }

}
