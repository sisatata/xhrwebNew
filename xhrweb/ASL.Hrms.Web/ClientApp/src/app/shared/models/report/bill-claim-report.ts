import { Guid } from "../guid";

export class BillClaimReport {

    id :any; 
    
    companyId :any; 
    startTime  :any;
    endTime:any;
    employeeId:any=Guid.empty;
   

public constructor (init?:Partial<BillClaimReport> )
{
  Object.assign(this,init);
}
}





