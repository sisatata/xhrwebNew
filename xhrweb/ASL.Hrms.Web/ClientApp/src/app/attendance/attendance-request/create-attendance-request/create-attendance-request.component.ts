import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { AttendanceRequest, Company, Guid } from 'src/app/shared/models';
import { AttendanceRequestService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import {RequestTypes} from 'src/app/enums/enum';
@Component({
  selector: 'app-create-attendance-request',
  templateUrl: './create-attendance-request.component.html',
  styleUrls: ['./create-attendance-request.component.css']
})
export class CreateAttendanceRequestComponent extends BaseAuthorizedComponent implements OnInit {
  onAttendanceRequestCreateEvent: EventEmitter<any> = new EventEmitter();
  onAttendanceRequestEditEvent: EventEmitter<any> = new EventEmitter();
  attendanceRequestCreateForm: FormGroup;
  submitted = false;
  companies: Company []= [];
  isEditMode = false;
  companyId: any = localStorage.getItem('companyId');
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  startDate: any;
  endDate: any;
  employeeId:string;
  time:any;
  requestType:number;
  requestTimePlaceHolder:string ='Request Time';
  startTimePlaceHolde:string = 'Start Time';
  requestTimeErrorPlaceHolder:string = 'Request time is required';
  requestTypes:any=[{id:1,value:'Late In'},{id:2,value:'Early Out'},{id:3,value:'Out Of Office'},{id:4,value:'Clock In Request'},{id:5,value:'Clock Out Request'}]
  attendanceRequest:AttendanceRequest;
  requestTime: string;
  requestDate: string;
  requestEndTime:any;
  showRequestEndDate:boolean = false;
  constructor(
    private dialogRef: MatDialogRef<CreateAttendanceRequestComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private datePipe: DatePipe,
    private injector: Injector,
    private attendanceRequestService: AttendanceRequestService,
    @Inject(MAT_DIALOG_DATA) data
  ) { 
    super(injector);
    this.attendanceRequest = new AttendanceRequest();
    if (isNaN(data)) {
      this.attendanceRequest = new AttendanceRequest(data);
   
     this.buildForm();
     if(this.attendanceRequest.requestTypeId === RequestTypes.OutOfOffice){
      this.attendanceRequestCreateForm.addControl('endTime',this.formBuilder.control(this.attendanceRequest.endTime, Validators.required));
      this.showRequestEndDate = true;
     }
     
      this.setRequestTime();
  // this.onChangeRequestType();
  this.buildForm();
    }
    if(this.attendanceRequest.id){
      this.isEditMode = true;
    }
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
   this.buildForm();
  }
setRequestTime():void{
  
  this.requestTime =  this.datePipe.transform(new Date(this.attendanceRequest.startTime), 'H:mm a') ;
  this.requestDate = this.datePipe.transform(new Date(this.attendanceRequest.startTime), 'yyyy/MM/dd')
this.requestDate =   this.attendanceRequest.startTime;

this.requestEndTime =  this.datePipe.transform(new Date(this.attendanceRequest.endTime), 'H:mm a') ;

 
}
  ngOnInit(): void {
    this.buildForm();
  }
  buildForm():void {
    this.attendanceRequestCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      employeeId:[this.employeeId,[Validators.required]],
      startDate: [this.attendanceRequest.startDate, [Validators.required]],
      endDate: [this.attendanceRequest.endDate],
      reason: [this.attendanceRequest.reason, [Validators.required, Validators.maxLength(150)]],
      requestTypeId:[this.attendanceRequest.requestTypeId,[Validators.required]],
      time:[this.time,[Validators.required]],
      endTime:[this.time],
    });
  }
  onSubmit():void{
    this.submitted = true;
    if(this.attendanceRequestCreateForm.invalid){
      return;
    }
    else if(this.attendanceRequest.id){
      this.editAttendanceRequest();
    }
    else{
      this.createAttendanceRequest();
    }
  }
  createAttendanceRequest():void{
   
 let time= this.convertTo24Hour(this.f.time.value);
 
 
   this.attendanceRequest = new AttendanceRequest(this.attendanceRequestCreateForm.value);
  
   this.attendanceRequest.startTime =  this.datePipe.transform(new Date(new Date(this.attendanceRequest.startDate).toDateString()+' '+time.toString()), 'MMM d, y, h:mm: a	');
  this.loading = true;
  this.attendanceRequest.startTime = this.datePipe.transform(new Date(this.attendanceRequest.startTime),'yyyy/MM/dd, h:mm:ss a');
 
  this.attendanceRequest.endTime = this.attendanceRequest.startTime;
  
  if(this.attendanceRequest.requestTypeId === RequestTypes.OutOfOffice){
    let endTime = this.convertTo24Hour(this.f.endTime.value);
   
    this.attendanceRequest.endTime =  this.datePipe.transform(new Date(new Date(this.attendanceRequest.startDate).toDateString()+' '+endTime.toString()), 'MMM d, y, h:mm: a	');
    this.attendanceRequest.endTime = this.datePipe.transform(new Date(this.attendanceRequest.endTime),'yyyy/MM/dd, h:mm:ss a');
  }
  this.attendanceRequestService.createAttendanceRequest(this.attendanceRequest).subscribe(res=>{
    if((res as any).status){
    this.onAttendanceRequestCreateEvent.emit(true);
               
              this.isFormValid = true;
               this.alertService.success('Attendance request create successful');
               this.close();
    }
    else{
       this.isFormValid = false;
       this.errorMessages = (res as any).message;
    }
    this.loading = false;    
  },()=>{
    this.loading = false;
  })
  
  }
  editAttendanceRequest():void{
    let newData = new AttendanceRequest(this.attendanceRequestCreateForm.value);
    if(this.attendanceRequest!== null){
      let time= this.convertTo24Hour(this.f.time.value);
     
      this.attendanceRequest.startTime =  this.datePipe.transform(new Date(new Date(newData.startDate).toDateString()+' '+time.toString()), 'MMM d, y, h:mm: a	');
   
      this.loading = true;
     
      this.attendanceRequest.startTime = this.datePipe.transform(new Date(this.attendanceRequest.startTime),'yyyy/MM/dd, h:mm:ss a');
      this.attendanceRequest.endTime = this.attendanceRequest.startTime;
    this.attendanceRequest.reason = newData.reason;
       
     
      this.loading = true
      if(this.attendanceRequest.requestTypeId === RequestTypes.OutOfOffice){
        let endTime = this.convertTo24Hour(this.attendanceRequestCreateForm.value.endTime);
      
        this.attendanceRequest.endTime =  this.datePipe.transform(new Date(new Date(newData.startDate).toDateString()+' '+endTime.toString()), 'MMM d, y, h:mm: a	');
        this.attendanceRequest.endTime = this.datePipe.transform(new Date(this.attendanceRequest.endTime),'yyyy/MM/dd, h:mm:ss a');
      }
      this.attendanceRequestService.editAttendanceRequest(this.attendanceRequest).subscribe(res=>{
        if((res as any).status){
          this.onAttendanceRequestEditEvent.emit(true);
                     
                    this.isFormValid = true;
                     this.alertService.success('Attendance request edit successful');
                     this.close();
          }
          else{
             this.isFormValid = false;
             this.errorMessages = (res as any).message;
          }
      },()=>{
        this.loading = false;
      })
    }
   
  }
   convertTo24Hour(time:any) {
     time = time.toString();
    
    var hours = parseInt(time.substr(0, 2));
    if(time.indexOf('am') != -1 && hours == 12) {
        time = time.replace('12', '0');
    }
    if(time.indexOf('pm')  != -1 && hours < 12) {
        time = time.replace(hours, (hours + 12));
    }
   
    return time.replace(/(am|pm)/, '');
}
  close():void{
    this.dialogRef.close();
  }
  onChangeRequestType():void{
    if(this.attendanceRequest.requestTypeId === RequestTypes.OutOfOffice){
        this.requestTimePlaceHolder = 'Start Time';
        this.requestTimeErrorPlaceHolder = 'Start time is required';
        this.attendanceRequestCreateForm.controls["endTime"].setValidators([Validators.required]);
        this.attendanceRequestCreateForm.controls["endTime"].setValue(null);
        this.requestEndTime = null;
        this.showRequestEndDate = true;
    }
    else{
      this.requestTimePlaceHolder = 'Request Time';
      this.requestTimeErrorPlaceHolder = 'Request time is required';
      this.attendanceRequestCreateForm.controls["endTime"].clearValidators();
      this.attendanceRequestCreateForm.controls["endTime"].setValue(null);
      this.requestEndTime = null;
      this.showRequestEndDate = false;;
    }
  }
  get f() { return this.attendanceRequestCreateForm.controls; }
}
