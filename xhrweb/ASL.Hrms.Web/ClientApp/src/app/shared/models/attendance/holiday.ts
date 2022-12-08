export class Holiday {
    id: any;
    companyId: any;
    holidayDate:any;
    reason: string = "";
    startDate:any;
    endDate:any;
    public constructor(init?: Partial<Holiday>) {
      Object.assign(this, init);
    }
  
  }
  