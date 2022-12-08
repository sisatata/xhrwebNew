export class EmployeePagedListInput{
    pageNumber:number;
    pageSize :number;
  
    public constructor(init?: Partial<EmployeePagedListInput>) {
      Object.assign(this, init);
    }
  }
  