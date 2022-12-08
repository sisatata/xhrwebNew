import { DatePipe } from '@angular/common';
import { Injector } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { timeStamp } from 'console';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { AttendanceRequest, Company } from 'src/app/shared/models';
import { AttendanceRequestService, CompanyService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ApproveOrRejectAttendanceRequestComponent } from './approve-or-reject-attendance-request/approve-or-reject-attendance-request.component';
import { CreateAttendanceRequestComponent } from './create-attendance-request/create-attendance-request.component';

@Component({
  selector: 'app-attendance-request',
  templateUrl: './attendance-request.component.html',
  styleUrls: ['./attendance-request.component.css']
})
export class AttendanceRequestComponent extends BaseAuthorizedComponent implements OnInit {
  companyId: any = localStorage.getItem('companyId');
  approvalStatuses:any[] = [{id:0,value:'All'},{id:'3',value:'Approved'},{id:'9',value:'Declined'},{id:'1',value:'Pending'}]
  attendanceRequestFilterFormGroup: FormGroup;
  employeeId: any;
  attendanceRequests:AttendanceRequest[]=[];
  attendanceRequest:AttendanceRequest = new AttendanceRequest();
  attendanceRequestList:AttendanceRequest[]=[];
  isReportingManager:boolean = false;
  loading:boolean = false;
  approveOrRejectAttendanceRequestModalData: any = {id:null,type:0};
  submitted:boolean = false;
  requestTypes:any=[{id:0,value:'All'},{id:1,value:'Late In'},{id:2,value:'Early Out'},{id:3,value:'Out Of Office'},{id:4,value:'Clock In Request'},{id:5,value:'Clock Out Request'}]
  companies: Company[]=[];
  startDate: any;
  isManagerMode:boolean = false;
  endDate: any;
  constructor(
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private dialog: MatDialog, private injector: Injector, private datePipe: DatePipe,
    private attendanceRequestService: AttendanceRequestService,
  ) {
    super(injector);
     this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
     
     this.isReportingManager = this.authService.isReportingManager;
    
     this.buildForm();
    
   }

  ngOnInit(): void {
    this.setDates();
  
   this.getAttendanceRequestsByEmployee();
   this.getAllCompanies();
  // this.buildForm();
  }
  buildForm() {
    this.attendanceRequestFilterFormGroup = this.formBuilder.group({
      startDate: [this.startDate, Validators.required],
      endDate: [this.endDate, Validators.required],
      employeeId: [this.employeeId],
      requestTypeId:[this.attendanceRequest.requestTypeId,[Validators.required]],
      approvalStatus:[''],
      companyId:[this.companyId]
      
    });
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  createAttendanceRequest():void{
    
      const createAttendanceRequestDialogConfig = new MatDialogConfig();
      // Setting different dialog configurations
      createAttendanceRequestDialogConfig.disableClose = true;
      createAttendanceRequestDialogConfig.autoFocus = true;
      createAttendanceRequestDialogConfig.panelClass = "xHR-dialog";
      const dialogWidth = window.screen.width <= 576 ? 90: 50;
      createAttendanceRequestDialogConfig.width = `${dialogWidth}%`;
      const createbranchDialog = this.dialog.open(CreateAttendanceRequestComponent, createAttendanceRequestDialogConfig);
       const successfullCreate = createbranchDialog.componentInstance.onAttendanceRequestCreateEvent.subscribe((data) => {
           this.getAttendanceRequestsByEmployee();
       });
      createbranchDialog.afterClosed().subscribe(() => {
      });
    
  }
  editAttendanceRequest(attendanceRequest: AttendanceRequest):void{
    
    const createAttendanceRequestDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createAttendanceRequestDialogConfig.disableClose = true;
    createAttendanceRequestDialogConfig.autoFocus = true;
    createAttendanceRequestDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createAttendanceRequestDialogConfig.width = `${dialogWidth}%`;
    createAttendanceRequestDialogConfig.data = attendanceRequest;
    const createbranchDialog = this.dialog.open(CreateAttendanceRequestComponent, createAttendanceRequestDialogConfig);
     const successfullCreate = createbranchDialog.componentInstance.onAttendanceRequestEditEvent.subscribe((data) => {
         this.getAttendanceRequestsByEmployee();
     });
    createbranchDialog.afterClosed().subscribe(() => {
    });
  
}
approveOrRejectAttendanceRequest(attendanceRequest: AttendanceRequest, type:number):void{
  const approveOrRejectAttendanceRequest = new MatDialogConfig();
  
  // Setting different dialog configurations
  approveOrRejectAttendanceRequest.disableClose = true;
  approveOrRejectAttendanceRequest.autoFocus = true;
  approveOrRejectAttendanceRequest.panelClass = "xHR-dialog";
  const dialogWidth = window.screen.width <= 576 ? 90: 50;
  approveOrRejectAttendanceRequest.width = `${dialogWidth}%`;
  this.approveOrRejectAttendanceRequestModalData.id = attendanceRequest.attendanceApplicationId;
  this.approveOrRejectAttendanceRequestModalData.type = type;
  approveOrRejectAttendanceRequest.data = this.approveOrRejectAttendanceRequestModalData;
  const createemployeePhoneDialog = this.dialog.open(ApproveOrRejectAttendanceRequestComponent, approveOrRejectAttendanceRequest);
  const successfullCreate = createemployeePhoneDialog.componentInstance.onAttendanceRequestApproveOrRejectCreateEvent.subscribe((data) => {
    this.getAttendanceRequestsByManager();
  });
  createemployeePhoneDialog.afterClosed().subscribe(() => {
  });  
}
  getAttendanceRequestsByManager():void{
    this.isManagerMode = true;
    this.loading = true;
    this.attendanceRequest = new AttendanceRequest(this.attendanceRequestFilterFormGroup.value);
    this.attendanceRequest.startTime = this.datePipe.transform(new Date(this.attendanceRequestFilterFormGroup.value.startDate), 'yyyy-MM-dd');
    this.attendanceRequest.endTime = this.datePipe.transform(new Date(this.attendanceRequestFilterFormGroup.value.endDate), 'yyyy-MM-dd');
    this.attendanceRequestService.getAttendanceRequestsByManagerId(this.employeeId, this.attendanceRequest.startTime, this.attendanceRequest.endTime).subscribe(res=>{
      this.attendanceRequests = res;
     this.totalItems = res.length;
     this.attendanceRequestList = res;
     this.generateTotalItemsText();
     if(this.attendanceRequestFilterFormGroup.value.approvalStatus!==''){
      this.filterDataOnChangApprovalStatus();
    }
      this.loading = false;
    },()=>{
      this.loading = false;
    })

  }
  getAttendanceRequestsByEmployee():void{
    this.isManagerMode = false;
    this.loading = true;
    this.attendanceRequest = new AttendanceRequest(this.attendanceRequestFilterFormGroup.value);
    this.attendanceRequest.startTime = this.datePipe.transform(new Date(this.attendanceRequestFilterFormGroup.value.startDate), 'yyyy-MM-dd');
    this.attendanceRequest.endTime = this.datePipe.transform(new Date(this.attendanceRequestFilterFormGroup.value.endDate), 'yyyy-MM-dd');
    this.attendanceRequest.requestTypeId = this.attendanceRequest.requestTypeId === null ? this.requestTypes[0].id : this.attendanceRequest.requestTypeId ;
    this.attendanceRequestService.getAttendanceRequestsByEmployeeId(this.employeeId,this.attendanceRequest.requestTypeId, this.attendanceRequest.startTime, this.attendanceRequest.endTime).subscribe(res=>{
                         this.loading = false;
                         this.attendanceRequests = res;
                         this.totalItems = res.length;
                         this.generateTotalItemsText();
                         this.attendanceRequestList = res;
                         if(this.attendanceRequestFilterFormGroup.value.approvalStatus!==''){
                           this.filterDataOnChangApprovalStatus();
                         }

    },()=>{
      this.loading = false;
    })
    
  }
  filterDataOnChangApprovalStatus():void{
    
    if(this.attendanceRequestFilterFormGroup.value.approvalStatus === '3' || this.attendanceRequestFilterFormGroup.value.approvalStatus === '9'){
      const data = this.attendanceRequestList.filter(x=>x.approvalStatus === this.attendanceRequestFilterFormGroup.value.approvalStatus.toString());
      this.attendanceRequests = data;
      this.totalItems = data.length;
      this.generateTotalItemsText();
      }
      else if(this.attendanceRequestFilterFormGroup.value.approvalStatus === '1'){
        const data = this.attendanceRequestList.filter(x=>x.approvalStatus !== '3' && x.approvalStatus !== '9');
        this.attendanceRequests = data;
        this.totalItems = data.length;
        this.generateTotalItemsText();
      }
      else{
        this.attendanceRequests = this.attendanceRequestList;
        this.totalItems = this.attendanceRequestList.length;
        this.generateTotalItemsText();
      }
  }
  setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    this.f.startDate.setValue(oneMonthAgoDate);
    this.f.endDate.setValue(today);
    if (this.isReportingManager) {
      this.f.startDate.setValue(oneDayAgoDate);
    }
   
  }
  get f() { return this.attendanceRequestFilterFormGroup.controls; }
}
