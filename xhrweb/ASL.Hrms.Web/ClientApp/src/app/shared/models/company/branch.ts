export class Branch {
    id:any;
    companyId:any;
    branchName:string="";
    branchLocalizedName:string="";
    shortName:string="";
    sortOrder:number = 1;
    public constructor (init?:Partial<Branch>){
        Object.assign(this,init);
    }

}
