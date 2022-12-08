export class Configuration {
  id: any;
  customConfigurationId: any;
  companyId: any;
  code: string = "";
  defaultValue: string = "";
  customValue: string = "";
  startDate: any;
  endDate: any;
 description: any;
  public constructor(init?: Partial<Configuration>) {
    Object.assign(this, init);
  }
}
