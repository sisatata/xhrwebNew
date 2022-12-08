    export class EmployeeBankAccount {

          id :any; 
          bankId :any; 
          bankBranchId :any; 
          accountNo :any; 
          accountTitle :any; 
          isPrimary :any = false; 
          companyId :any; 
          employeeId :any; 
          startDate :any; 
          endDate :any; 
          remarks :any; 
          bankName: any;
          branchName: any;
          fullName: any;
 public constructor (init?:Partial< EmployeeBankAccount > )
    {
        Object.assign(this,init);
    }
}



