    export class SalaryStructure {

          id :any; 
          structureName :any; 
          description :any; 
          startDate :any; 
          endDate :any; 
          isDeleted :any; 
          companyId :any; 
          listSal: SalaryStructure[];

 public constructor (init?:Partial< SalaryStructure > )
    {
        Object.assign(this,init);
    }
}



