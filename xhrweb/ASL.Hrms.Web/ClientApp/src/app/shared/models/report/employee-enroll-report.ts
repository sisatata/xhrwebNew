export class EmployeeEnrollReport {
    id: any;
    companyId: any;
    startDate: any;
    endDate: any;
    type: any;
    public constructor(init?: Partial<EmployeeEnrollReport>) {
        Object.assign(this, init);
    }
}
