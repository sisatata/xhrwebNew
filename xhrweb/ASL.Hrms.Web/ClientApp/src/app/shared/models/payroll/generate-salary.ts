export class GenerateSalary {
    id:any;
    companyId:any;
    financialYearId:any;
    monthCycleId:any;
    employeeId:any;
    processingStartDate:any;
    processingEndDate:any;
    public constructor(init?:Partial<GenerateSalary>){
        Object.assign(this,init);
    }

}