export class TaskDetailLog {

    id :any; 
    taskDetailId:any;
    updateInfo:string;
    dateUpdated:any;
    employeeId:any;
    employeeName:string;
    startDate:any;
    endDate:any;
    spendTime:any;

public constructor (init?:Partial< TaskDetailLog > )
{
  Object.assign(this,init);
}
}



