export class IncomeTaxParameter {

    id: any;
    parameterName: any;
    limitAmount: any;
    limitPercentageOfBasic: any;
    startDate: any;
    endDate: any;
    remarks: any;
    isActive: boolean = false;
    isDeleted: any;
    companyId: any;
    payerTypeCode: any;
    public constructor(init?: Partial<IncomeTaxParameter>) {
        Object.assign(this, init);
    }
}



