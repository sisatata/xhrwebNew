export class CustomConfiguration {
  id: any;
  companyId: any;
  code: string = "";
  defaultValue: string = "";
  customValue: string = "";
  startDate: any;
  endDate: any;
  description: any;
  public constructor(init?: Partial<CustomConfiguration>) {
    Object.assign(this, init);
  }
}
