import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Company, TaskReport } from 'src/app/shared/models';
import { CompanyService, EmployeeService, TaskReportService } from 'src/app/shared/services';

@Component({
  selector: 'app-task-report',
  templateUrl: './task-report.component.html',
  styleUrls: ['./task-report.component.css']
})
export class TaskReportComponent implements OnInit {
  taskReportFilterFormGroup: FormGroup;
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  taskReport: TaskReport = new TaskReport();
  companyId: any = localStorage.getItem('companyId');
  submitted: boolean;
  isFormValid: boolean;
  employees: any;
  employeeId: any;
  loading: boolean = false;
  isAdmin: boolean = false;
  isEmployee: boolean = false;
  startDate: any;
  endDate: any;
  companies : Company[];
  pdfUrl: any;
  itemList : any= [];
  settings = {};
  empId: string;
  constructor(
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private taskReportService: TaskReportService,
    private authService: AuthService,
    private sanitize: DomSanitizer,
  ) { 
    this.isEmployee = this.authService.isEmployee;
    this.isAdmin = this.authService.isAdmin;
    this.empId = this.authService.getLoggedInUserInfo().employeeId;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
  }

  ngOnInit(): void {
    this.getAllCompanies();
    this.getAllEmployees();
    this.buildForm();
    this.initializeEmployeeDropdown();
  }
  buildForm():void {
    this.taskReportFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.startDate, [Validators.required]],
      endDate: [this.endDate, [Validators.required]],
      employeeId: [this.employeeId]
     
    });
  }

  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAllEmployees():void {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.itemList = result;
      const employee = this.employees.filter(x=>x.id === this.employeeId)[0];
      this.selectedItems.push(employee);
    },()=>{});
  }
  onSubmit(): void {
    this.submitted = true;
   
    if (this.taskReportFilterFormGroup.invalid) {
      return;
    }
    this.getTaskReport();
  }
  
  
  getTaskReport():void{
    this.taskReport = new TaskReport(this.taskReportFilterFormGroup.value); 
    const employeeId = this.selectedItems.length === 0 ? this.empId: this.selectedItems[0].id;
    this.taskReport.employeeId = employeeId;
    this.loading = true;
    this.taskReportService.createTaskPdfReport(this.taskReport).subscribe(res=>{
           this.loading = false;
           let url = ((res as any).printPdfUrl);
           this.pdfUrl = this.sanitize.bypassSecurityTrustResourceUrl(url);
   },()=>{
   this.loading = false;
   })
  }
  initializeEmployeeDropdown():void{
    this.selectedItems = [];
    const width = window.screen.width > 768 ? '2': '1';
    this.settings = {
      text: "Select employees",
      enableSearchFilter: true,
      singleSelection: true,
      unSelectAllText:"Un Select All",
      classes: "myclass custom-class",
      labelKey:"fullName",
      primaryKey:"id",
      badgeShowLimit:width,
      showCheckbox:true,
      enableFilterSelectAll:true,
      disabled: (this.isAdmin && this.isEmployee) === true? false:true ,
      maxHeight:"200"
      
  };
}
get f() { return this.taskReportFilterFormGroup.controls; }
}
