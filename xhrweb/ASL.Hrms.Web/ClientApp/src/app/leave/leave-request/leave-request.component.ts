import { DatePipe } from '@angular/common';
import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { Company, LeaveRequest } from 'src/app/shared/models';
import { CompanyService, LeaveApplicationService } from 'src/app/shared/services';
import { ApproveOrRejectLeaveRequestComponent } from './approve-or-reject-leave-request/approve-or-reject-leave-request.component';

@Component({
  selector: 'app-leave-request',
  templateUrl: './leave-request.component.html',
  styleUrls: ['./leave-request.component.css']
})
export class LeaveRequestComponent extends BaseAuthorizedComponent implements OnInit {
  leaveRequestFilterFormGroup: FormGroup;
  leaveRequests: LeaveRequest[]=[];
  leaveRequest: LeaveRequest= new LeaveRequest();
  leaveRequestList: LeaveRequest[]=[];
  submitted:boolean = false;
  companyId:any = localStorage.getItem('companyId');
  companies: Company[]=[];
  approvalStatus:string ='';
  loading:boolean = false;
  approveOrRejectLeaveRequestModalData: any = {id:null,type:0};
  managerId: string;
  approvalStatuses:any[] = [{id:0,value:'All'},{id:'3',value:'Approved'},{id:'9',value:'Declined'},{id:'1',value:'Pending'}]
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private leaveApplicationService: LeaveApplicationService,
    private datePipe: DatePipe,
    private injector : Injector) { 
      super(injector);
      this.managerId = this.authService.getLoggedInUserInfo().employeeId;
    }

  ngOnInit(): void {
    this.buildForm();
    this.setDates();
    this.getAllLeaveRequestsByManager();
    this.getAllCompanies();

  }
  buildForm():void {
    this.leaveRequestFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      startDate:[this.leaveRequest.startDate,[Validators.required]],
      endDate:[this.leaveRequest.endDate,[Validators.required]],
      approvalStatus:[this.leaveRequest.approvalStatus]
    });
  }
  getAllLeaveRequestsByManager():void{
    this.loading = true;
    this.leaveRequest = new LeaveRequest(this.leaveRequestFilterFormGroup.value);
    this.leaveRequest.startDate = this.datePipe.transform(new Date(this.leaveRequest.startDate), 'yyyy-MM-dd');
    this.leaveRequest.endDate = this.datePipe.transform(new Date(this.leaveRequest.endDate), 'yyyy-MM-dd');
    this.leaveApplicationService.getLeaveRequestsByManagerId(this.managerId, this.leaveRequest.startDate, this.leaveRequest.endDate).subscribe(res=>{
                      this.loading = false;
                      this.leaveRequests = res;
                      this.leaveRequestList = res;
                      this.totalItems = res.length
                      this.generateTotalItemsText();
                     if(this.leaveRequestFilterFormGroup.value.approvalStatus){
                       this.filterDataOnChangApprovalStatus();
                     }
    },()=>{
      this.loading = false;
    })
    
    
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    const stDate = oneMonthAgoDate;
    const edDate = oneDayAgoDate;
    this.f.startDate.setValue(new Date(stDate));
    this.f.endDate.setValue(new Date(edDate));
   
  }
 
  filterDataOnChangApprovalStatus():void{
    if(this.leaveRequestFilterFormGroup.value.approvalStatus === '3' || this.leaveRequestFilterFormGroup.value.approvalStatus === '9'){
      const data = this.leaveRequestList.filter(x=>x.approvalStatus === this.leaveRequestFilterFormGroup.value.approvalStatus.toString());
      this.leaveRequests = data;
      this.totalItems = data.length;
      this.generateTotalItemsText();
      }
      else if(this.leaveRequestFilterFormGroup.value.approvalStatus === '1'){
        const data = this.leaveRequestList.filter(x=>x.approvalStatus !== '3' && x.approvalStatus !== '9');
        this.leaveRequests = data;
        this.totalItems = data.length;
        this.generateTotalItemsText();
      }
      else{
        this.leaveRequests = this.leaveRequestList;
        this.totalItems = this.leaveRequestList.length;
        this.generateTotalItemsText();
      }
  }
  approveOrRejectLeaveRequest(leaveRequest: LeaveRequest, type:number):void{
    const approveOrRejectLeaveRequest = new MatDialogConfig();
    console.log(leaveRequest)
    
    // Setting different dialog configurations
    approveOrRejectLeaveRequest.disableClose = true;
    approveOrRejectLeaveRequest.autoFocus = true;
    approveOrRejectLeaveRequest.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    approveOrRejectLeaveRequest.width = `${dialogWidth}%`;
    this.approveOrRejectLeaveRequestModalData.id = leaveRequest.leaveApplicationId;
    this.approveOrRejectLeaveRequestModalData.type = type;
    approveOrRejectLeaveRequest.data = this.approveOrRejectLeaveRequestModalData;
    const createemployeePhoneDialog = this.dialog.open(ApproveOrRejectLeaveRequestComponent, approveOrRejectLeaveRequest);
    const successfullCreate = createemployeePhoneDialog.componentInstance.onleaveRequestApproveOrRejectCreateEvent.subscribe((data) => {
      this.getAllLeaveRequestsByManager();
    });
    createemployeePhoneDialog.afterClosed().subscribe(() => {
    });  
  }
  get f() { return this.leaveRequestFilterFormGroup.controls; }
}
