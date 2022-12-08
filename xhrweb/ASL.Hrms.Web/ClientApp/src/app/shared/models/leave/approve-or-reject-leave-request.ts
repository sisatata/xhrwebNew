export class ApproveOrRejectLeaveRequest {
   applicationId:any;
   notes:string='';
    public constructor(init?: Partial<ApproveOrRejectLeaveRequest>) {
      Object.assign(this, init);
    }
  
  }
  