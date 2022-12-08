export class TaskCategory {

    id :any; 
    taskCategoryName:string;
    remarks:string;
    companyId:any;

public constructor (init?:Partial< TaskCategory > )
{
  Object.assign(this,init);
}
}



