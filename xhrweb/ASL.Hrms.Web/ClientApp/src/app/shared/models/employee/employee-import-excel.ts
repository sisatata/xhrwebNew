export class Employeeimport {
    id: any;
    fileName:string;
    fileType:string;
    comments:string;
    companyId: any ;
    
    public constructor(init?: Partial<Employeeimport>){
      Object.assign(this, init);
    }
  }
  