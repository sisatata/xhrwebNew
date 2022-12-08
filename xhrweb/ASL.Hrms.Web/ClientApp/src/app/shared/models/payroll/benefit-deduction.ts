export class BenefitDeductionCode {

    id: any;
    companyId: any;
    benifitDeductionCode: any;
    sortOrder: any = 1 ;
    benifitDeductionCodeName: any;
    
  
    public constructor(init?: Partial<BenefitDeductionCode>) {
      Object.assign(this, init);
    }
  }
  
  
  
  