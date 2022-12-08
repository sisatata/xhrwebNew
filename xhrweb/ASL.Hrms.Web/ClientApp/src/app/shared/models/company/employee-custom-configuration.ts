export class CustomEmployeeConfiguration {
    id: any;
    companyId: any;
    employeeId:any;
    code: string = "";
    defaultValue: string = "";
    customValue: string = "";
    startDate: any;
    endDate: any;
    description: any;
   
    public constructor(init?: Partial<CustomEmployeeConfiguration>) {
      Object.assign(this, init);
    }
  }
  