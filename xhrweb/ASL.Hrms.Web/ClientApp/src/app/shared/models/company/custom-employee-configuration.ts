export class CustomEmployeeConfiguration {
    id: any;
    companyId: any;
    code: string = "";
    value:any;
    defaultValue: string = "";
    customValue: string = "";
    startDate: any;
    endDate: any;
    description: any;
    public constructor(init?: Partial<CustomEmployeeConfiguration>) {
      Object.assign(this, init);
    }
  }
  