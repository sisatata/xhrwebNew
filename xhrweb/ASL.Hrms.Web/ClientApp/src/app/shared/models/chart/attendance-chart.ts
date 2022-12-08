export class AttendanceChart {
    id:any;
    totalPresent: any;
    totalLate: any;
    totalAbsent: any;
    totalLeave: any;
    public constructor (init?:Partial<AttendanceChart>){
        Object.assign(this,init);
    }

}
