export class LeaveProcess {
  id: any;
  companyId: any;
  employeeId: any;
  processingStartDate: any;
  processingEndDate: any;
    leaveCalendarId: any;

  public constructor(init?: Partial<LeaveProcess>) {
    Object.assign(this, init);
  }

}
