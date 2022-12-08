export class ApproveOrRejectAttendanceRequest {
    id:any;
    note:string='';
     public constructor(init?: Partial<ApproveOrRejectAttendanceRequest>) {
       Object.assign(this, init);
     }
   
   }
   