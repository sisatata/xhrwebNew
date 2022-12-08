export class SalaryRule {
  id: any;
  companyId: any;
  ruleName: string = "";
  description: string = "";
  startDate: any;
  endDate: any;
  public constructor(init?: Partial<SalaryRule>) {
    Object.assign(this, init);
  }
}
