export class Shift {
    id:any;
    shiftGroupId:any;
    shiftName:string="";
    shiftLocalizedName:string="";
    shiftCode:string="";
    isActive:boolean=true;
    shiftIn:any;
    shiftOut:any;
    shiftLate:any;
    lunchBreakIn:any;
    lunchBreakOut:any;
    earlyOut:any;
    regHour:any;
    ramadanIn:any;
    ramadanOut:any;
    ramadanLate:any;
    ramadanEarlyOut:any;
    ramadanLunchBreakIn:any;
    ramadanLunchBreakOut:any;
    startTime:string="";
    endTime:string="";
    graceIn:number=0;
    graceOut:number=0;
    range:number=0;
    isRollingShift:boolean= false;
    isRelaborShift:boolean= false;
    companyId:any;
    isDeleted:boolean= false;
    public constructor(init?:Partial<Shift>){
     Object.assign(this,init);
    }
}
