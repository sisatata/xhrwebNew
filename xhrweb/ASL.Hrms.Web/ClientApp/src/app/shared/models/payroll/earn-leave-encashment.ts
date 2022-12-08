export class EarnLeaveEncashment {
  id: any;
  companyId: any;
  employeeId: any;
  leaveTypeId: any;
  encashDate: any;
  financialYearId: any;
  monthCycleId: any;
  elEncashedDays: number;
  remarks: string;
  leaveTypeName:string;
  fullName:string;
  monthCycleName:string;
  financialYearName:string;
  encashedAmount:number;
  public constructor(init?: Partial<EarnLeaveEncashment>) {
    Object.assign(this, init);
  }
}
