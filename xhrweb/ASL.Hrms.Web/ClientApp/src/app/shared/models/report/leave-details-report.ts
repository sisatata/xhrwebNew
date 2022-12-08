export class LeaveDetailsReport {

    id :any; 
    companyId :any; 
    branchIds:any;
    departmentIds:any;
    DesignationIds:any;
    startDate  :any;
    endDate:any;
    endTime:any;
    approvalStatusText: any;
    employeeIds:any;
    statusId:any;
public constructor (init?:Partial<LeaveDetailsReport> )
{
  Object.assign(this,init);
}
}



