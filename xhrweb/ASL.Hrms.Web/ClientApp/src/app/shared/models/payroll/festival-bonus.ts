export class FestivalBonus {
  id: any;
  companyId: any;
  religionId: any;
  rangeFromInMonth: any;
  rangeToInMonth: any;
  basicOrGrossId: any;
  percentOfBasicOrGross: any;
  fixedAmount: any;
  isPaidFull: boolean = false;
  partialOnId: any;
  startDate: any;
  endDate: any;
  remarks: any;
  public constructor(init?: Partial<FestivalBonus>) {
    Object.assign(this, init);
  }
}



