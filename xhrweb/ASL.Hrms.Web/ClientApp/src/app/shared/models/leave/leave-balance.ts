export class LeaveBalance {
    id:any;
    employeeId: any;
    companyId:any;
    employeeName: any;
    leaveTypeId:any;
    leaveTypeName: any;
    leaveCalendar: any;
    maximumBalance: any;
    usedBalance:any;
    remainingBalance: any;
    branchIds: any;
    branchName: string = "";
    departmentIds: any;
    departmentName: string = "";
    designationIds: any;
    positionName: string = "";
    companyEmployeeId: string ="";
    searchText:string = '';
   public constructor(init?:Partial<LeaveBalance>){
       Object.assign(this,init);
   }
}
