export class Company {
  id: any;
  companyName: number;
  shortName: number;
  companyLocalizedName: any;
  companySlogan: any;
  licenseKey: any;
  sortOrder: any = 1;
  isActive: boolean=false;
  userId: string = "";
  password: string = "";
  approvalStatus: string = "";
  approvalStatusText: string = "";
  EmployeFullName :string="";
  companyWebsite :string="";
  facebookLink :string="";
  public constructor(init?: Partial<Company>) {
    Object.assign(this, init);
  }
}
