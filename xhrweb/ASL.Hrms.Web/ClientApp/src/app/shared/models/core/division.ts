export class Division {
  id: any;
  companyId: number;
  divisionName: number;
  divisionNameLC: any;
  sortOrder: any;
  isActive: boolean;
  public constructor(init?: Partial<Division>) {
    Object.assign(this, init);
  }
}
