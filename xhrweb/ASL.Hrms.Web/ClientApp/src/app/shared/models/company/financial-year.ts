export class FinancialYear {
    id:any;
    companyId:any;
    financialYearName:string="";
    financialYearLocalizedName:string="";
    financialYearStartDate:any;
    financialYearEndDate:any;
    isRunningYear:boolean=false;
    sortOrder:any = 1;
    isDeleted:boolean;
    public constructor(init?:Partial<FinancialYear>){
        Object.assign(this,init);
    }
}
