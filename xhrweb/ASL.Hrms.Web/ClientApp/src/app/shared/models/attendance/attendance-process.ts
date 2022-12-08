export class AttendanceProcess {
    id:any;
    companyId:any;
    attendanceCalendarId:any;
    employeeId:any;
    processingStartDate:any;
    processingEndDate:any;
    public constructor(init?:Partial<AttendanceProcess>){
        Object.assign(this,init);
    }

}
