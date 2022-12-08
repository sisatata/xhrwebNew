export class ApproveBillClaim {
    id: any;
    approvedAmount:number;
    approvedNotes:string;
    claimAmount:number;
    benefitBillClaimId:any;
    billAttachmentUri: string;
    public constructor(init?: Partial<ApproveBillClaim>) {
        Object.assign(this, init);
    }
}
