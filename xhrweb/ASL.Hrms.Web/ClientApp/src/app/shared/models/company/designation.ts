export class Designation {
  id: any;
  companyId: any;
  branchId: any;
  departmentId: any;
  linkedEntityId: any;
  linkedEntityType: string = "";
  designationName: string = "";
  shortName: string = "";
  designationLocalizedName: string = "";
  sortOrder: number;
  isActive: boolean;
  public constructor(init?: Partial<Designation>) {
    Object.assign(this, init);
  }

}
