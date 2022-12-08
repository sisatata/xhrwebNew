export class ProvidentFundReport {
    id: any;
    companyId: any;
    financialYearId:any;
    monthCycleId:any
    employeeId:any;
    public constructor(init?: Partial<ProvidentFundReport>) {
        Object.assign(this, init);
    }
}
