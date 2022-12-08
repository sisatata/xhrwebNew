export class ConfirmationRule {
  id: any;
  companyId: any;
  ruleName: string = "";
  description: string = "";
  startDate: any;
  endDate: any;
  confirmationType: number;
  confirmationMonths: number;
  sortOrder: number;
  isActive: boolean;
  public constructor(init?: Partial<ConfirmationRule>) {
    Object.assign(this, init);
  }
}
