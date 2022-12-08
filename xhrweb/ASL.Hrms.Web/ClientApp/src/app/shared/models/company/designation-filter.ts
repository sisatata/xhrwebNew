export class DesignationFilter {
    companyId: any;
    entityIds: string[];
    public constructor(init?: Partial<DesignationFilter>) {
        Object.assign(this, init);
    }
}
