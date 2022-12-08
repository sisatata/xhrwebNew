    export class CompanyPhone {

          id :any; 
          companyId :any; 
          phoneNumber :any; 
          phoneTypeId :any; 

 public constructor (init?:Partial< CompanyPhone > )
    {
        Object.assign(this,init);
    }
}



