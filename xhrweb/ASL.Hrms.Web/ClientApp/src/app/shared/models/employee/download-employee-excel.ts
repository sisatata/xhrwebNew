export class DownloadEmployeeImportExcelFile {
    id: any;
    fileName:string;
    companyId:any;
        
    public constructor(init?: Partial<DownloadEmployeeImportExcelFile>){
      Object.assign(this, init);
    }
  }
  