export class IncomeTaxInvestment {

    id: any;
    investmentPercentage: number;
    waiverPercentage: number;
    startDate: any;
    endDate: any;
    remarks: string;
    companyId: any;
    public constructor(init?: Partial<IncomeTaxInvestment>) {
        Object.assign(this, init);
    }
}



