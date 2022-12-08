export class Department {
  id: any;
  companyId: any;
  branchId: any;
  departmentName: string="";
  shortName: string = "";
  departmentLocalizedName: string = "";
  sortOrder: number=1;
  isActive: boolean;
  public constructor(init?: Partial<Department>) {
    Object.assign(this, init);
  }
}
