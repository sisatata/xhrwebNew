export class DepartmentFilter {
  
    companyId: any;
    branchIds:string[];
    public constructor(init?: Partial<DepartmentFilter>) {
      Object.assign(this, init);
    }
  }
  