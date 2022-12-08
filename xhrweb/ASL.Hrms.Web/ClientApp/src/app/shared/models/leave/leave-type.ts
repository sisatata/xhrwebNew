export class LeaveType {
    id:any;
    companyId:any;
    leaveTypeName:string="";
    leaveTypeShortName:string="";
    leaveTypeLocalizedName:string="";
    balance:number=0;
    isCarryForward:boolean=false;
    isFemaleSpecific:boolean=false;
    isPaid:boolean=false;
    isEncashable:boolean=false;
    consecutiveLimit:number=0;
    encashReserveBalance:number=0;
    isDependOnDateOfConfirmation:boolean=false;
    isLeaveDaysFixed:boolean=false;
    isBasedWorkingDays:boolean=false;
    isAllowAdvanceLeaveApply:boolean=false;
    isAllowWithPrecedingHoliday:boolean=false;
    isAllowOpeningBalance:boolean=false;
    isPreventPostLeaveApply: boolean = false;
    leaveTypeGroupId: any = 0;
    leaveTypeGroupName: string = "";
    public constructor (init?:Partial<LeaveType>){
        Object.assign(this,init);
    }

}
