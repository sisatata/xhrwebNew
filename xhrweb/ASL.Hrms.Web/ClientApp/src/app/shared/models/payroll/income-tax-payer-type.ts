export class IncomeTaxPayerType {

    id :any; 
    payerTypeCode: string;
    payerTypeName: string;
    remarks: string;
    isActive: boolean=false;
   
    companyId: any;
public constructor (init?:Partial< IncomeTaxPayerType > )
{
  Object.assign(this,init);
}
}



