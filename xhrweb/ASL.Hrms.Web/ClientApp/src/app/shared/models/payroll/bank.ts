export class Bank {

  id: any;
  bankName: any;
  bankNameLC: any;
  sortOrder: any = 1 ;
  companyId: any;
  isDeleted: any;
  paymentOptionId: any;

  public constructor(init?: Partial<Bank>) {
    Object.assign(this, init);
  }
}



