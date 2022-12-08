export class ConfirmationReport {

    id :any; 
    
    companyId :any; 
    startDate  :any;
    endDate:any;
    employeeId:any
   

public constructor (init?:Partial<ConfirmationReport> )
{
  Object.assign(this,init);
}
}



