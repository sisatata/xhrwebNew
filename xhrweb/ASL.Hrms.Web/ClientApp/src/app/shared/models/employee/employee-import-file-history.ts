export class EmployeeImportFile {
    id: any;
    fileName:string;
    fileType:string;
    comments:string;
    companyId:any;
    status:string;
    totalFail:number;
    totalRecord:number;
    totalSuccess:number;
   
    public constructor(init?: Partial<EmployeeImportFile>){
      Object.assign(this, init);
    }
  }
  