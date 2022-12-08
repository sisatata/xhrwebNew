export class LeaveReport {
    id: any;
    companyId: any;
    startDate: any;
    endDate: any;
    leaveTypeName: any;
    employeeName: any;
    approvalStatusText: any;
    statusId:any;
    public constructor(init?: Partial<LeaveReport>) {
        Object.assign(this, init);
    }
}
