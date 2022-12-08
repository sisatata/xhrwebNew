import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Attendance } from 'src/app/shared/models/attendance/attendance';
import { AttendanceService } from 'src/app/shared/services/attendance/attendance.service';
import { AttendanceProcessData } from 'src/app/shared/models/attendance/attendance-process-data';
import { AttendanceProcessDataService } from 'src/app/shared/services/attendance/attendance-process-data.service';
import { CompanyService, BranchService, EmployeeService } from 'src/app/shared/services';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { CreateEmployeeAttendanceComponent } from './create-employee-attendance/create-employee-attendance.component';
import { DatePipe } from '@angular/common';
import { SearchFilterComponent } from 'src/app/shared/components/search-filter/search-filter.component';
import { AttendanceProcessFilter } from 'src/app/shared/models';
import { EmployeeAttendanceDetailsComponent } from './employee-attendance-details/employee-attendance-details.component';
@Component({
  selector: 'app-employee-attendance',
  templateUrl: './employee-attendance.component.html',
  styleUrls: ['./employee-attendance.component.css']
})
export class EmployeeAttendanceComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('attendance-list-section') blockUI: NgBlockUI;
  @ViewChild(SearchFilterComponent) searchFilter:SearchFilterComponent;
  attendanceProcessDatas: AttendanceProcessData[] = [];
  attendanceProcessFilter: AttendanceProcessFilter = new AttendanceProcessFilter();
  attendanceId: any;
  branchId: any;
  companies: any;
  branches: any;
  attendanceFilterFormGroup: FormGroup;
  submitted: boolean;
  endDate: any;
  startDate: any;
  employeeId: any;
  isAdmin: any;
  startDateInitialValue: any;
  endDateInitialValue: any;
  companyId: any = localStorage.getItem('companyId');
  employees: any;
  loading: boolean = false;
  searched: boolean = true;
  inputValue:string='';
  allEmployees:any;
  constructor(private attendanceService: AttendanceService,
    private attendanceProcessDataService: AttendanceProcessDataService, private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private dialog: MatDialog, private injector: Injector, private datePipe: DatePipe) {
    super(injector);
    this.isAdmin = this.authService.isAdmin;
    //this.isAdmin = false;
    this.buildForm();
  }
  ngOnInit() {
    this.getAllEmployees();
    //this.buildForm()
    this.setDates();
    
    
    //this.getAttendanceDataByEmployeeId();
  }
  ngAfterViewInit():void{
    if (this.isAdmin) {
      this.search();
    }
    else {
      this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
      this.getAttendanceDataByEmployeeId();
    }
    this.buildForm();
    this.setDates();
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
      this.employees.unshift({ id: 1, fullName: 'Select Employee' });
    });
  }
  buildForm() {
    this.attendanceFilterFormGroup = this.formBuilder.group({
      startDate: [this.startDate, Validators.required],
      endDate: [this.endDate, Validators.required],
      employeeId: [this.employeeId],
      companyId: [this.companyId]
    });
  }
  get f() { return this.attendanceFilterFormGroup.controls; }
  getAttendanceData() {
    this.submitted = true;
    if (this.attendanceFilterFormGroup.invalid) {
      return;
    }
    
    if (this.attendanceFilterFormGroup.value.employeeId === null || this.attendanceFilterFormGroup.value.employeeId === '1') {
      this.getAttendanceDataByCompanyId();
    }
    else {
      this.getAttendanceDataByEmployeeId();
    }
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAttendanceDataByCompanyId(): void {
    this.blockUI.start();
    this.startDate = this.f.startDate.value;
    this.endDate = this.f.endDate.value;
    this.startDate = this.datePipe.transform(new Date(this.startDate), 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(new Date(this.endDate), 'yyyy-MM-dd');
    this.loading = true;
 
    this.attendanceProcessDataService.getAttendanceProcessDataByCompany(this.companyId, this.startDate, this.endDate).subscribe(result => {
      this.searched = true;  // no status
     
      this.attendanceProcessDatas = result;
      this.allEmployees = result;
      this.totalItems = this.attendanceProcessDatas.length;
      this.loading= false;
      this.generateTotalItemsText();
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.emptyTable();
    })
  }
  onChangeEmployee() {
    this.employeeId = this.f.employeeId.value;
    this.emptyTable();
  }
  onChangeStartDate(): void {
    this.emptyTable();
  }
  onChangeEndDate(): void {
    this.emptyTable();
  }
  getAttendanceDataByEmployeeId() {
    this.blockUI.start();
    this.startDate = this.f.startDate.value;
    this.endDate = this.f.endDate.value;
    this.startDate = this.datePipe.transform(new Date(this.startDate), 'yyyy-MM-dd');
    this.endDate = this.datePipe.transform(new Date(this.endDate), 'yyyy-MM-dd');
    this.loading =true;
    this.attendanceProcessDataService.getAttendanceProcessDataByEmployee(this.employeeId, this.startDate, this.endDate).subscribe(result => {
      this.attendanceProcessDatas = result; // no status
     
      this.totalItems = this.attendanceProcessDatas.length;
      this.searched = true;
      this.loading = false;
      this.generateTotalItemsText();
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.emptyTable();
    })
  }
  createAttendance() {
    const createattendanceDialogConfig = new MatDialogConfig;
    createattendanceDialogConfig.disableClose = true;
    createattendanceDialogConfig.autoFocus = true;
    createattendanceDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createattendanceDialogConfig.width = `${dialogWidth}%`;
    var attendance = new Attendance();
    //attendance.companyId = this.companyId;
    createattendanceDialogConfig.data = attendance;
    const createAttendanceDialog = this.dialog.open(CreateEmployeeAttendanceComponent, createattendanceDialogConfig)
    const successfullCreate = createAttendanceDialog.componentInstance.onAttendanceCreateEvent.subscribe((data) => {
      if (this.isAdmin) {
        this.f.employeeId.setValue(null);
        this.getAttendanceDataByCompanyId();
      }
      else {
        this.getAttendanceDataByEmployeeId();
      }
    });
    createAttendanceDialog.afterClosed().subscribe(() => {
    });
  }
  editAttendance(attendance: Attendance) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = attendance
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateEmployeeAttendanceComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onAttendanceEditEvent.subscribe((data) => {
      this.getAttendanceDataByEmployeeId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteAttendance(attendance: Attendance): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the attendance " + attendance.attendanceDate;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteAttendance(attendance);
      }
    });
  }
  deleteAttendance(attendance: Attendance) {
    this.attendanceService.deleteAttendance(attendance).subscribe((data) => {
      this.getAttendanceDataByEmployeeId();
    },
      (error) => {
        console.log(error);
      });
  }
  setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    this.f.startDate.setValue(oneMonthAgoDate);
    this.f.endDate.setValue(today);
    if (this.isAdmin) {
      this.f.startDate.setValue(oneDayAgoDate);
    }
  }
  emptyTable(): void {
    this.attendanceProcessDatas = [];
    this.totalItems = 0;
    this.searched = false;
    this.loading = false;
  }
  onInputChange(data:string):void{
    this.attendanceProcessDatas =[];
    data = data.toLowerCase();
    this.attendanceProcessDatas = this.allEmployees.filter(item=> item.employeeName.toLowerCase().includes(data));
    this.totalItems = this.attendanceProcessDatas.length;
    this.generateTotalItemsText();
  }
  search(searched?:boolean): void {
    if(!this.isAdmin){
      this.getAttendanceDataByEmployeeId();
      return;
    }
    if(this.attendanceFilterFormGroup.invalid){
      return;

    }
    let form = this.searchFilter.employeeFilterFormGroup;
    this.loading = true;
    this.attendanceProcessFilter.branchIds = form.value.branchId;
    this.attendanceProcessFilter.departmentIds = form.value.departmentId;
    this.attendanceProcessFilter.designationIds = form.value.designationId;
    this.attendanceProcessFilter.searchText = form.value.searchText;
    this.attendanceProcessFilter.startDate = this.datePipe.transform(new Date(this.attendanceFilterFormGroup.value.startDate), 'yyyy-MM-dd');
    this.attendanceProcessFilter.endDate = this.datePipe.transform(new Date(this.attendanceFilterFormGroup.value.endDate), 'yyyy-MM-dd');
    this.attendanceProcessFilter.companyId = this.attendanceFilterFormGroup.value.companyId;
   
    this.attendanceProcessDataService
      .getEmployeeAttendanceData(this.attendanceProcessFilter)
      .subscribe(
        (res) => {
         this.attendanceProcessDatas = (res as any);
          this.loading = false;
          this.totalItems = (res as any).length;
        
          if(searched===true)
          this.currentPage = 1;
          this.generateTotalItemsText();
        },
        () => {}
      );
  }
  
  openEmployeeDetails(data:Attendance):void{
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = data;
    dialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    dialogConfig.width = `${dialogWidth}%`;
    dialogConfig.height="600px";
    const outletEditDialog = this.dialog.open(EmployeeAttendanceDetailsComponent, dialogConfig);
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
}
