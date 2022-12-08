export class CompanyEmail {
  id: any;
 // companyName: number;
  companyId:any;
  emailAddress: any;
  remarks:any;
  isPrimary:any = false;
  public constructor(init?: Partial<CompanyEmail>) {
    Object.assign(this, init);
  }
}
