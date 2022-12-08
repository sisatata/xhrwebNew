export class EmployeeFilter {
    branchIds: any;
    departmentIds: any;
    designationIds: any;
    employeeIds:any;
    searchText:string = '';
    pageNumber:number;
    pageSize:number;
    public constructor(init?: Partial<EmployeeFilter>) {
        Object.assign(this, init);
    }
}
