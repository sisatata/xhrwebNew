export class Grade {
  id: any;
  companyId: any;
  gradeName: string = "";
  gradeNameLocalized: string = "";
  rank: number;
  public constructor(init?: Partial<Grade>) {
    Object.assign(this, init);
  }
}
