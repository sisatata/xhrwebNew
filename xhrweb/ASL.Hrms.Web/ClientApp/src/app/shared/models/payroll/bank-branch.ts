    export class BankBranch {

          id :any; 
          branchName :any; 
          branchNameLC :any; 
          branchAddress :any; 
          contactPerson :any; 
          contactNumber :any; 
          contactEmailId :any; 
          sortOrder :any; 
          companyId :any; 
          isDeleted: any;
          bankId: any;

 public constructor (init?:Partial< BankBranch > )
    {
        Object.assign(this,init);
    }
}



