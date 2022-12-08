import { Guid } from "../guid";
export class BonusReport {
  id: any;
  companyId: any;
  financialYearId: any;
  bonusTypeId: any;
  employeeId:any;
  public constructor(init?: Partial<BonusReport>) {
    Object.assign(this, init);
  }
}
