    export class SalaryStructureComp {

          id :any; 
          salaryStrutureId :any; 
          salaryComponentName :any; 
          value :any; 
          valueType :any; 
          percentOn :any; 
      companyId: any;
      valueTypeText: any;
      percentOnText: any; 

 public constructor (init?:Partial< SalaryStructureComp > )
    {
        Object.assign(this,init);
    }
}



