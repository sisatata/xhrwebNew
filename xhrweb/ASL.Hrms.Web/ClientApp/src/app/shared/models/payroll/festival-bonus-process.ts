export class FestivalBonusProcess {
    id: any;
    companyId: any;
    religionId: any;
    bonusTypeId?:any;
    bonusDate?:any;
    financialYearId?:any;
    public constructor(init?: Partial<FestivalBonusProcess>) {
      Object.assign(this, init);
    }
  }
  
  
  
  