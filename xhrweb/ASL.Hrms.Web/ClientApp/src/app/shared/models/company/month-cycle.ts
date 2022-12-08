export class MonthCycle {
    id:any;
    companyId:any;
    financialYearId:any;
    monthCycleName:string="";
    monthCycleLocalizedName:string="";
    monthStartDate:any;
    monthEndDate:any;
    totalWorkingDays:any;
    runningFlag:boolean = false;
    isSelected:boolean;
    sortOrder:any = 1;
    public constructor (init?:Partial<MonthCycle>){
        Object.assign(this,init);
    }
}
