import { TaskDetailService } from "../../services";

export class TaskDetail {

    id :any; 
    taskTypeId:any;
    taskName:any;
    companyId:any;
    assigneeId:any;
    parentTaskId:any;
    taskDescription: string;
    startDate:any;
    endDate: any;
    fullName:any;
    listTaskDetail: TaskDetail[];
    statusId:any;
    progress:any;
    assignedBy:any;
    taskCreator:any;
    assignedById:any;
  

public constructor (init?:Partial< TaskDetail > )
{
  Object.assign(this,init);
}
}



