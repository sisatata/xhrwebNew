export class AttendanceProcessFilter {
    branchIds: any;
    departmentIds: any;
    designationIds: any;
    employeeIds:any;
    searchText:string = '';
    startDate:any;
    endDate:any;
    companyId:any;
    public constructor(init?: Partial<AttendanceProcessFilter>) {
        Object.assign(this, init);
    }
}
