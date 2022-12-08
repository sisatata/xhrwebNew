export class PayrollChart {
  
    grossSalary:any;
    payableSalary:any;
    netPayableAmount:any;
    totalDeductedAmount:any;
    public constructor (init?:Partial<PayrollChart>){
        Object.assign(this,init);
    }

}
