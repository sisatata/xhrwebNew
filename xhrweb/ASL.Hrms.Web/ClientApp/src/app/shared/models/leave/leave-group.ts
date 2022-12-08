export class LeaveGroup {
    id: any;
    companyId: any;
    leaveTypeGroupName: string = "";
    leaveTypeGroupNameLC: string = "";
    public constructor(init?: Partial<LeaveGroup>) {
      Object.assign(this, init);
    }
  
  }
  