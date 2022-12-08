export class EmployeeCard {
    id: any;
    employeeId: any;
    cardMaskingValue: string = "";
    issueDate: any;
    chargeAmount: number;
    isPaid: boolean = false;
    paymentDate: any;
    cardRevoked: boolean= false ;
    cardRevokedDate: any;
    companyId?: any;
    public constructor(init?: Partial<EmployeeCard>) {
      Object.assign(this, init);
    }
  }
  