import { Guid } from "../guid";

export class SalaryReport {

    id :any; 
    companyId :any; 
   financialYearId:any;
   monthCycleId:any;
   employeeId:any;
   

public constructor (init?:Partial<SalaryReport> )
{
  Object.assign(this,init);
}
}



