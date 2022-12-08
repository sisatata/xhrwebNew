export class ShiftGroup {
  id: any;
  companyId: any;
  shiftGroupName: string = "";
  shiftGroupNameLC: string = "";
  public constructor(init?: Partial<ShiftGroup>) {
    Object.assign(this, init);
  }

}
