import { DatePipe } from '@angular/common';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ConfirmationReport, Guid } from 'src/app/shared/models';
import {  CompanyService, EmployeeEnrollReportService, EmployeeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-confirmation-report',
  templateUrl: './confirmation-report.component.html',
  styleUrls: ['./confirmation-report.component.css']
})
export class ConfirmationReportComponent implements OnInit {
  confirmationReportFilterFormGroup: FormGroup;
  companies: any;
  companyId: any = localStorage.getItem('companyId');
  submitted: boolean;
  incomeTaxSlabs: any;
  loading: boolean;
  startTime: any;
  endTime: any;
  isFormValid: boolean;
  employees: any;
  employeeId: any = Guid.empty;
  confirmationReport: ConfirmationReport;
  isAdmin: any;
  isEmployee: any;
  pdfUrl: any;
  startDate: any;
  endDate: any;
  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private injector: Injector,
    private datePipe: DatePipe,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
    
    private confirmationReportService:EmployeeEnrollReportService
  ) { }

  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
  }
  buildForm() {
    this.confirmationReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.startDate, [Validators.required]],
      endDate: [this.endDate, [Validators.required]]
      
    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    },()=>{})
  }
  get f() { return this.confirmationReportFilterFormGroup.controls; }
  onSubmit(): void {
    this.submitted = true;
    console.log(this.confirmationReportFilterFormGroup);
    if (this.confirmationReportFilterFormGroup.invalid) {
      return;
    }
    this.createAndGetConfirmationReport();
  }
  createAndGetConfirmationReport():void{
    this.loading = true;
    this.confirmationReport = new ConfirmationReport(this.confirmationReportFilterFormGroup.value);
    this.confirmationReport.startDate = this.datePipe.transform(new Date(this.confirmationReport.startDate), 'yyyy-MM-dd');
    this.confirmationReport.endDate = this.datePipe.transform(new Date(this.confirmationReport.endDate), 'yyyy-MM-dd');
    this.confirmationReportService.createConfirmationPdfReport(this.confirmationReport).subscribe(res=>{
      this.loading = false;
      let url = ((res as any).printPdfUrl);
      this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
    },()=>{
      this.loading = false;
    })

  }
}
