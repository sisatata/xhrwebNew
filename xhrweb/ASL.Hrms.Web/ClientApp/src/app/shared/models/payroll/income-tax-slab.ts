export class IncomeTaxSlab {

    id:any;
    slabName: string;
    description: string;
    startDate: any;
    endDate: any;
    isRunningFlag: boolean = false;
    companyId: any;
    slabAmount: any;
    taxablePercent: any;
    taxPayerTypeCode: any;
    taxPayerTypeName:any;
    slabOrder: number = 1;
    public constructor(init?: Partial<IncomeTaxSlab>) {
        Object.assign(this, init);
    }
}



