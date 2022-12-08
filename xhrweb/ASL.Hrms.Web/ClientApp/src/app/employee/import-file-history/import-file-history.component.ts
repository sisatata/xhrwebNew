import { ThrowStmt } from '@angular/compiler';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { Company, DownloadEmployeeImportExcelFile, EmployeeImportFile } from 'src/app/shared/models';
import { CompanyService, FileService } from 'src/app/shared/services';
import { ImportEmployeeComponent } from '../import-employee/import-employee.component';
@Component({
  selector: 'app-import-file-history',
  templateUrl: './import-file-history.component.html',
  styleUrls: ['./import-file-history.component.css']
})
export class ImportFileHistoryComponent  extends BaseAuthorizedComponent implements OnInit {
companyId:any = localStorage.getItem('companyId');
employeeFileHistoryFilterFormGroup: FormGroup;
employeeImportFiles:EmployeeImportFile [] ;
loading:boolean = false;
downloadEmployeeImportExcelFile: DownloadEmployeeImportExcelFile ;
companies: Company[];
  constructor(
    private fileService: FileService,
    private injector: Injector,
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private companyService: CompanyService,
  ) { 
    super(injector)
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllEmployeeFileHistory();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  buildForm():void {
    this.employeeFileHistoryFilterFormGroup = this.formBuilder.group({
    companyId: [this.companyId, [Validators.required]],
    });
  }
  getAllEmployeeFileHistory():void{
    this.loading =true;
    this.fileService.getRawFileDataByCompanyId(this.companyId).subscribe(res=>{
             this.loading = false;
             this.employeeImportFiles = res;
             this.totalItems = res.length;
             this.generateTotalItemsText();
             
    },()=>{
      this.loading = false;
    })
  }
  downloadFileHistoryDetails({id}:any):void{
    this.loading = true;
    this.downloadEmployeeImportExcelFile = new DownloadEmployeeImportExcelFile();
    this.downloadEmployeeImportExcelFile.id = id;
    this.fileService.downloadEmployeeImportFileHistory(this.downloadEmployeeImportExcelFile).subscribe(res=>{
              console.log(res);
           this.generateExcel(res,'Employee-import-history');
           this.loading =false;
    },()=>{
      this.loading = false;
    })
  }
  importEmployee():void{
    const importDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    importDialogConfig.disableClose = true;
    importDialogConfig.autoFocus = true;
   
    const dialogWidth = window.screen.width <= 576 ? 50 : 50;
    importDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      ImportEmployeeComponent,
      importDialogConfig
    );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  generateExcel(result: any, fileName: any): void {
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
  get f() { return this.employeeFileHistoryFilterFormGroup.controls; }        
}
