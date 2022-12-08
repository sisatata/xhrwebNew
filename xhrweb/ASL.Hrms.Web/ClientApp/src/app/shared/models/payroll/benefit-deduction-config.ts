export class BenefitDeductionConfig {

    id:any; 
    benefitDeductionCode:any; 
    name:any;
    description:any;
    type:any; 
    basicOrGross:any;
    calculationType: any;
    percentOfBasicOrGross:any; 
    fixedAmount: any;
    intervalId: any;
    companyId: any;
    isCalculateSalary: any= false; 
    isActive: any= false;
    startDate: any ;
    endDate: any ;
    
  
    public constructor(init?: Partial<BenefitDeductionConfig>) {
      Object.assign(this, init);
    }
  }
  
  
  