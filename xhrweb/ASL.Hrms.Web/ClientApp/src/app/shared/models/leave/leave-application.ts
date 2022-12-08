export class LeaveApplication {
  id: any;
  companyId: any;
  branchId: any;
  employeeId: any;
  leaveTypeId: any;
  leaveCalendar: string = "";
  startDate: any;
  endDate: any;
  noOfDays: any;
  reason: string = "";
  employeeName: string = "";
  leaveTypeName: any;
  notes: string = "";
  applyDate: any;
  branchIds: any;
  branchName: string = "";
  departmentIds: any;
  departmentName: string = "";
  designationIds: any;
  positionName: string = "";
  companyEmployeeId: string = "";
  searchText: string = "";
  approvalStatusText: string = '';
  approvalStatus: string = '';
  public constructor(init?: Partial<LeaveApplication>) {
    Object.assign(this, init);
  }
}
