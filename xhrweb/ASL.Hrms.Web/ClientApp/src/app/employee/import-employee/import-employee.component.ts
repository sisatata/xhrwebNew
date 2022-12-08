import { Component, EventEmitter, OnInit } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { Employeeimport } from "src/app/shared/models";
import { EmployeeService, FileService } from "src/app/shared/services";
import { AlertService } from "src/app/shared/services/alert.service";
@Component({
  selector: "app-import-employee",
  templateUrl: "./import-employee.component.html",
  styleUrls: ["./import-employee.component.css"],
})
export class ImportEmployeeComponent implements OnInit {
  onEmployeeEmportCreateEvent: EventEmitter<boolean>;
  companyId: any = localStorage.getItem("companyId");
  loading: boolean = false;
  excelFile: any;
  fileChoosed: boolean = false;
  file: File;
  employeeimport: Employeeimport;
  isFormValid: boolean = true;
  errorMessages: string;
  comments:string;
  constructor(
    private alertService: AlertService,
    private employeeService: EmployeeService,
    private fileService: FileService,
    private router: Router,
    private dialogRef: MatDialogRef<ImportEmployeeComponent>
  ) {}
  ngOnInit(): void {}
  uploadExcel(file: any): void {
     this.isFormValid = true;
    this.file = <File>file[0];
    
    if( this.file && this.file.size)
      this.fileChoosed = true;
    else {
     
      this.fileChoosed = false
    }
  }
  downloadTemplate():void{
    this.loading = true;
    this.fileService.downloadEmployeeExcelTemplate(null).subscribe(res=>{
       this.generateExcel(res, 'Employee-Template');
      this.loading = false;
    },()=>
    {
      this.loading = false;
    })
  }
  close(): void {
    this.dialogRef.close();
  }
  onSubmit(): void {
    this.uploadEmployeeExcel();
  }
  uploadEmployeeExcel(): void {
    var formData = new FormData();
    formData.append("file", this.file, this.file.name);
    formData.append("companyId", this.companyId);
    formData.append("fileType", this.file.type);
    formData.append("fileName", this.file.name);
    formData.append("comments", this.comments);
    this.loading = true;
    this.fileService.importEmployeeExcel(formData).subscribe(
      (res) => {
        this.loading = false;
        if((res as any).status === true){
          this.isFormValid = true;
        this.alertService.success('Employee list imported successfully');
        this.close();
        }
        else{
          this.isFormValid = false;
          this.errorMessages = (res as any).message;
        }
      },
      () => {
        this.loading = false;
      }
    );
  }
  generateExcel(result: any, fileName: string): void {
    if (window.navigator.msSaveOrOpenBlob) {
      window.navigator.msSaveBlob(result.body, fileName);
    } else {
      const link = window.URL.createObjectURL(result.body);
      const a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = link;
      a.download = fileName;
      a.click();
      window.URL.revokeObjectURL(link);
      a.remove();
    }
  }
  importFileHistory():void{
    this.router.navigate([ '/employee/employee-file-import-history' ]);
    this.dialogRef.close();
  }
}
