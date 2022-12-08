export class ApproveOrRejectEmployeePromotionTransfer {
  employeePromotionTransferId: any;
  notes: string = "";
  public constructor(init?: Partial<ApproveOrRejectEmployeePromotionTransfer>) {
    Object.assign(this, init);
  }
}
