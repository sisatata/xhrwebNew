export class AttendanceRequest {
    
       id:any;
       attendanceApplicationId:any;
       employeeId:any;
       applicantName:string='';
       requestTypeText:string='';
       requestTypeId:number;
       startTime:any;
       endTime:any;
       startDate:any;
       endDate:any;
       reason:string='';
       approvalStatusText:string='';
       approvalStatus:string='';
       notes:string='';
       companyId:any;
    public constructor(init?: Partial<AttendanceRequest>) {
      Object.assign(this, init);
    }
  
  }
  