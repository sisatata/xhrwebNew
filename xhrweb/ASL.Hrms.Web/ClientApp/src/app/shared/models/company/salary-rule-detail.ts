export class SalaryRuleDetail {
  id: any;
  salaryRuleId: any;
  salaryHead: string = "";
  value: string = "";
  ValueType: string = "";
  percentDependOn: string = "";
  public constructor(init?: Partial<SalaryRuleDetail>) {
    Object.assign(this, init);
  }
}
