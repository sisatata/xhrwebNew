export class AttendanceDetailsReport {

    id :any; 
    companyId :any; 
    branchIds:any;
    departmentIds:any;
    DesignationIds:any;
    startDate  :any;
    endDate:any;
    endTime:any;
    type:any;
    employeeIds:any;
    statusId:any;
public constructor (init?:Partial<AttendanceDetailsReport> )
{
  Object.assign(this,init);
}
}



