export class BenefitDeductionEmployee {

    id: any;
    benefitDeductionId:any;
    employeeId:any
    remarks:any;
    startDate:any;
    endDate:any;
    amount:any;
    public constructor(init?: Partial<BenefitDeductionEmployee>) {
      Object.assign(this, init);
    }
  }
  
  
  
  