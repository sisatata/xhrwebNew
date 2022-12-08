import { Guid } from "../guid";
export class PayslipReport {
    id: any;
    companyId: any;
    financialYearId: any;
    monthCycleId: any;
    employeeId:any= Guid.empty;

    public constructor(init?: Partial<PayslipReport>) {
        Object.assign(this, init);
    }
}



