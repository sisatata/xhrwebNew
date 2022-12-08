import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { OTSummaryReport } from 'src/app/shared/models';
import { CompanyService, OTSummaryReportService } from 'src/app/shared/services';

@Component({
  selector: 'app-ot-summary-report',
  templateUrl: './ot-summary-report.component.html',
  styleUrls: ['./ot-summary-report.component.css']
})
export class OtSummaryReportComponent implements OnInit {
  otSummaryReportFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  otSummaryReport: OTSummaryReport;
  loading: boolean = false;
  companies: any;
  financialYears: any;
  pdfUrl: any;
  downloaded: boolean = false;
  isEmployee: any;
  submitted: boolean;
  employees: any;
  employeeId: any;
  isAdmin: any;
  startDate: Date;
  endDate: Date;
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private sanitize: DomSanitizer,
    private datePipe: DatePipe,
    private otSummaryReportService: OTSummaryReportService
  ) { }

  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
  }
  buildForm(): void {
    this.otSummaryReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.startDate, [Validators.required]],
      endDate: [this.endDate, [Validators.required]],
      
    });
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    },()=>{

    })
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.otSummaryReportFilterFormGroup.invalid) {
      return;
    }
    this.createAndOTSummaryReport();
  }
  createAndOTSummaryReport():void{
    this.loading = true;
    this.otSummaryReport = new OTSummaryReport(this.otSummaryReportFilterFormGroup.value);
    this.otSummaryReport.startDate = this.datePipe.transform(new Date(this.otSummaryReport.startDate), 'yyyy-MM-dd');
    this.otSummaryReport.endDate = this.datePipe.transform(new Date(this.otSummaryReport.endDate), 'yyyy-MM-dd');
    this.otSummaryReportService.createOTSummaryPdfReport(this.otSummaryReport).subscribe(res=>{
               this.loading = false;
               const  url = ((res as any).printPdfUrl);
               this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);  
    }, ()=>{
       this.loading = false;
    })
    
  }
  get f() { return this.otSummaryReportFilterFormGroup.controls; }

}
