export class TaskReport {
    id: any;
    companyId: any;
    startDate: any;
    endDate: any;
    employeeId:any;
    public constructor(init?: Partial<TaskReport>) {
        Object.assign(this, init);
    }
}
