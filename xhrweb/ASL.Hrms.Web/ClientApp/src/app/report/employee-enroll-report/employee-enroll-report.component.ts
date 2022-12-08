import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import { EmployeeEnrollReport } from 'src/app/shared/models';
import { CompanyService, EmployeeEnrollReportService } from 'src/app/shared/services';

@Component({
  selector: 'app-employee-enroll-report',
  templateUrl: './employee-enroll-report.component.html',
  styleUrls: ['./employee-enroll-report.component.css']
})
export class EmployeeEnrollReportComponent implements OnInit {
  EmployeeEnrollReportFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  loading: boolean = false;
  companies: any;
  submitted: boolean = false;
  employeeEnrollReport: EmployeeEnrollReport;
  employeeId: any;
  employees: any;
  pdfUrl:any;
  downloaded:boolean=false;
  isEmployee: any;
  payrollManager:any;
  isAdmin: any;
  types:any = [{id:10, value:"Join"},{id:20,value:'Quit'}]
  startDate: any;
  endDate: any;
 // form value 
  type: any;
   // for set initial type value
  typeId: any;
  defaultTypeId:any = 10;;
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
    private employeeEnrollmentReportService: EmployeeEnrollReportService,
    private datePipe:DatePipe
  ) { }

  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.typeId = this.defaultTypeId;
    
  }
  buildForm(): void {
    this.EmployeeEnrollReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.startDate, [Validators.required]],
      endDate: [this.endDate, [Validators.required]],
      type: [this.type]
    });
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    },()=>{

    })
  }
  onSubmit():void{
    this.submitted = true;
    if(this.EmployeeEnrollReportFilterFormGroup.invalid){
      return;
    }
    this.createAndGetEmployeeEnrollReport();
  }
  createAndGetEmployeeEnrollReport():void{
    this.employeeEnrollReport = new EmployeeEnrollReport(this.EmployeeEnrollReportFilterFormGroup.value);
    this.employeeEnrollReport.startDate = this.datePipe.transform(new Date(this.employeeEnrollReport.startDate), 'yyyy-MM-dd');
    this.employeeEnrollReport.endDate = this.datePipe.transform(new Date(this.employeeEnrollReport.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.employeeEnrollmentReportService.createEmployeeEnrollPdfReport(this.employeeEnrollReport).subscribe(res=>{
      let url = ((res as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
      this.loading = false;
    },()=>{

    })
  }
  get f() { return this.EmployeeEnrollReportFilterFormGroup.controls; }
}
