export class OTSummaryReport {
    id: any;
    companyId: string;
    startDate: string;
    endDate: string;
    public constructor(init?: Partial<OTSummaryReport>) {
        Object.assign(this, init);
    }
}
