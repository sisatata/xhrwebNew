export class LeaveRequest {
  id: any;
  leaveApplicationId:any;
  managerId: any;
  startDate: any;
  endDate: any;
  applicantName:string ='';
  applyDate:any;
  noOfDays:number;
  leaveTypeName:string='';
  notes:string='';
  maximumBalance:number;
  usedBalance: number;
  remainingBalance:number;
  reason:string='';
  addressOnLeave:string='';
  emergencyContact:string='';
  approvalStatusText:string=''
  approvalStatus:string='';
  public constructor(init?: Partial<LeaveRequest>) {
    Object.assign(this, init);
  }
}
