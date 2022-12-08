export class EmployeeExperience {
  id: any;
  employeeId: any;
  companyName: string = "";
  designation: string = "";
  functionalDesignation: string = "";
  department: string="";
  responsibilities: string = "";
  companyAddress: string = "";
  companyContactNo: string = "";
  companyMobile: string = "";
  companyEmail: string = "";
  companyWeb: string="";
  fromDate: any;
  toDate: any;
  tillDate: boolean;

  public constructor(init?: Partial<EmployeeExperience>) {
    Object.assign(this, init);
  }
}

