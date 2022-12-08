import { Guid } from "../guid";

export class EarnLeaveEncashmentReport {
  id: any;
  companyId: any;
  financialYearId: any;
  monthCycleId: any;
  
  public constructor(init?: Partial<EarnLeaveEncashmentReport>) {
    Object.assign(this, init);
  }
}
