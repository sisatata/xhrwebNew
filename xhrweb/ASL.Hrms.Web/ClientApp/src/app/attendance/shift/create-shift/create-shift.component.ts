import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Shift } from 'src/app/shared/models/attendance/shift';
import { CompanyService, ShiftService, ShiftGroupService } from 'src/app/shared/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from '../../../shared/services/alert.service';
import { DatePipe } from '@angular/common';



@Component({
  selector: 'app-create-shift',
  templateUrl: './create-shift.component.html',
  styleUrls: ['./create-shift.component.css']
})
export class CreateShiftComponent implements OnInit {
  onShiftCreateEvent: EventEmitter<any> = new EventEmitter();
  onShiftEditEvent: EventEmitter<any> = new EventEmitter();
  shiftCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  shiftGroups: any;
  shiftGroupId: any;
  shift: Shift;
  shiftId: number;
  isEditMode = false;

  shiftIn: any;
  shiftOut: any;
  shiftLate: any;

  lunchBreakIn: any;
  lunchBreakOut: any;

  earlyOut: any;
  regHour: any;

  ramadanIn: any;
  ramadanOut: any;
  ramadanLate: any;
  ramadanEarlyOut: any;
  ramadanLunchBreakIn: any;
  ramadanLunchBreakOut: any;
  today = new Date();
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
isShiftDuplicate:boolean;
  shifts: Shift[];
  constructor(
    private companyService: CompanyService,
    public shiftGroupService: ShiftGroupService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateShiftComponent>,
    private formBuilder: FormBuilder,
    private shiftService: ShiftService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.shift = new Shift();
    if (isNaN(data)) {
      this.shift = new Shift(data);
      console.log(this.shift);
      this.setTimePickerValues();
      this.companyId = this.shift.companyId;
      if (this.shift.id)
        this.isEditMode = true;

    } else {
    }
    this.buildForm();
    this.getAllCompanies();
    this.getAllShiftGroupsByCompany(this.companyId);
    //this.isEditMode = false;
    //console.log(this.isEditMode);
  }

  setTimePickerValues() {
    //this.datePipe.transform(date,'MM-dd-yyyy hh-mm-ss' );

    this.shiftIn = this.shift && this.shift.shiftIn ? this.datePipe.transform(new Date(this.shift.shiftIn), 'H:mm') : '';
    console.log(this.shiftIn)
    this.shiftOut = this.shift && this.shift.shiftOut ? new Date(this.shift.shiftOut).toLocaleTimeString() : '';
    this.shiftLate = this.shift && this.shift.shiftLate ? new Date(this.shift.shiftLate).toLocaleTimeString() : '';
    this.lunchBreakIn = this.shift && this.shift.lunchBreakIn ? new Date(this.shift.lunchBreakIn).toLocaleTimeString() : '';
    this.lunchBreakOut = this.shift && this.shift.lunchBreakOut ? new Date(this.shift.lunchBreakOut).toLocaleTimeString() : '';
    this.earlyOut = this.shift && this.shift.earlyOut ? new Date(this.shift.earlyOut).toLocaleTimeString() : '';
    this.regHour = this.shift && this.shift.regHour ? this.datePipe.transform(new Date(this.shift.regHour), 'H:mm') : '';// new Date(this.shift.regHour).toLocaleTimeString('H:mm') : '';
    this.ramadanIn = this.shift && this.shift.ramadanIn ? new Date(this.shift.ramadanIn).toLocaleTimeString() : '';
    this.ramadanOut = this.shift && this.shift.ramadanOut ? new Date(this.shift.ramadanOut).toLocaleTimeString() : '';
    this.ramadanLate = this.shift && this.shift.ramadanLate ? new Date(this.shift.ramadanLate).toLocaleTimeString() : '';
    this.ramadanEarlyOut = this.shift && this.shift.ramadanEarlyOut ? new Date(this.shift.ramadanEarlyOut).toLocaleTimeString() : '';
    this.ramadanLunchBreakIn = this.shift && this.shift.ramadanLunchBreakIn ? new Date(this.shift.ramadanLunchBreakIn).toLocaleTimeString() : '';
    this.ramadanLunchBreakOut = this.shift && this.shift.ramadanLunchBreakOut ? new Date(this.shift.ramadanLunchBreakOut).toLocaleTimeString() : '';
  }

  ngOnInit() {
    this.getAllCompanies();
    this.getAllShiftGroupsByCompany(this.companyId);
    this.getAllShiftByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  getAllShiftGroupsByCompany(companyId) {
    if (this.companyId) {
      this.shiftGroupService.getShiftGroupByCompanyId(companyId).subscribe(result => {
        this.shiftGroups = result;
      });
    }
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId !== null) {
      this.getAllShiftGroupsByCompany(this.companyId);
    }
  }
  onChangeShifGroup() {
    this.shiftGroupId = this.f.shiftGroupId.value;
  }


  buildForm() {
    this.shiftCreateForm = this.formBuilder.group({
      companyId: [this.shift.companyId, [Validators.required]],
      shiftGroupId: [this.shift.shiftGroupId, [Validators.required]],
      shiftName: [this.shift.shiftName, [Validators.required, Validators.maxLength(250)]],
      shiftLocalizedName: [this.shift.shiftLocalizedName, [Validators.maxLength(230)]],
      shiftCode: [this.shift.shiftCode, [Validators.required, Validators.maxLength(3)]],
      shiftIn: [this.shiftIn, [Validators.required]],
      shiftOut: [this.shiftOut, [Validators.required]],
      shiftLate: [this.shiftLate, [Validators.required]],
      lunchBreakIn: [this.lunchBreakIn, [Validators.required]],
      lunchBreakOut: [this.lunchBreakOut, [Validators.required]],
      earlyOut: [this.earlyOut, [Validators.required]],
      regHour: [this.regHour, [Validators.required]],
      ramadanIn: [this.ramadanIn, [Validators.required]],
      ramadanOut: [this.ramadanOut, [Validators.required]],
      ramadanLate: [this.ramadanLate, [Validators.required]],
      ramadanEarlyOut: [this.ramadanEarlyOut, [Validators.required]],
      ramadanLunchBreakIn: [this.ramadanLunchBreakIn, [Validators.required]],
      ramadanLunchBreakOut: [this.ramadanLunchBreakOut, [Validators.required]],
      startTime: [this.shift.startTime],
      endTime: [this.shift.endTime],
      graceIn: [this.shift.graceIn],
      graceOut: [this.shift.graceOut],
      range: [this.shift.range],
      isRollingShift: [this.shift.isRollingShift],
      isRelaborShift: [this.shift.isRelaborShift],
      isActive: [this.shift.isActive],
      effectiveFromTime: [''],

    });
  }

  onSubmit() {
    this.submitted = true;
   // console.log(this.shiftCreateForm.value)
    if(this.checkDuplicateShiftCode())
{
  this.isShiftDuplicate = true;
  return;
}    // stop here if form is invalid
    if (this.shiftCreateForm.invalid) {
      return;
    }
    if (this.shift.id === undefined) {
      this.createShift();
    }
    else {
      this.editShift();
    }
   // this.dialogRef.close();
  }

  createShift() {
    this.shift = new Shift(this.shiftCreateForm.value);
    this.setShiftInOutTimeForSubmit(this.shift);
   this.loading = true;
    this.shiftService.createShift(this.shift).subscribe((data: any) => {
      this.onShiftCreateEvent.emit(this.shift.id);
     
      if((data as any).status === true){
        this.isFormValid = true;
        this.alertService.success("Shift added successfully");
        this.dialogRef.close();

      }
      else{
        this.isFormValid = false;
        this.errorMessages = (data as  any).message;
      }
     this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;

    });

  }
  editShift() {

    let changeData = new Shift(this.shiftCreateForm.value);
    if (this.shift !== null) {
      this.shift.companyId = changeData.companyId;
      this.shift.shiftGroupId = changeData.shiftGroupId;
      this.shift.shiftName = changeData.shiftName;
      this.shift.shiftLocalizedName = changeData.shiftLocalizedName;
      this.shift.shiftCode = changeData.shiftCode;

      this.setShiftInOutTimeForSubmit(changeData);

      this.shift.startTime = changeData.startTime;
      this.shift.endTime = changeData.endTime;
      this.shift.graceIn = changeData.graceIn;
      this.shift.graceOut = changeData.graceOut;
      this.shift.range = changeData.range;
      this.shift.isRollingShift = changeData.isRollingShift;
      this.shift.isRelaborShift = changeData.isRelaborShift;
      this.shift.isActive = changeData.isActive;
      this.shift.isDeleted = changeData.isDeleted;
      this.loading = true;
      this.shiftService.editShift(this.shift).subscribe((data: any) => {
        this.onShiftEditEvent.emit(this.shift.id)
        
        if((data as any).status === true){
          this.isFormValid = true;
          this.alertService.success("Shift updated successfully");
          this.dialogRef.close();

        }
        else{
          this.isFormValid = false;
          this.errorMessages = (data as  any).message;
        }
       this.loading = false;
      }, (error: any) => {
        this.showErrorMessage(error);
        this.loading = false;
      });
    }
  }

  setShiftInOutTimeForSubmit(changeData: Shift) {
    //alert (new Date (new Date().toDateString() + ' ' + '10:55'))


    this.shift.shiftIn = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.shiftIn), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.shiftOut = this.shift.shiftOut ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.shiftOut), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.shiftLate = this.shift.shiftLate ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.shiftLate), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.lunchBreakIn = this.shift.lunchBreakIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.lunchBreakIn), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.lunchBreakOut = this.shift.lunchBreakOut ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.lunchBreakOut), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.earlyOut = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.earlyOut), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.regHour = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.regHour), 'MMM d, y, h:mm	') : '';
    this.shift.ramadanIn = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.ramadanIn), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.ramadanOut = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.ramadanOut), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.ramadanLate = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.ramadanLate), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.ramadanLunchBreakIn = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.ramadanLunchBreakIn), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.ramadanLunchBreakOut = this.shift.shiftIn ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.ramadanLunchBreakOut), 'MMM d, y, h:mm:ss a	') : '';
    this.shift.ramadanEarlyOut = this.shift.ramadanEarlyOut ? this.datePipe.transform(new Date(new Date().toDateString() + ' ' + changeData.ramadanEarlyOut), 'MMM d, y, h:mm:ss a	') : '';

  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.shiftCreateForm.controls; }
  getAllShiftByCompanyId() {
   
    this.loading = true;
    this.shiftService.getAllShiftByCompany(this.companyId).subscribe(result => {
      this.shifts = result;
      this.loading = false;
 
      

     
    }, error => {
     
      this.loading = false;

    })

  }
  checkDuplicateShiftCode():boolean{
    let shiftCode = this.shiftCreateForm.value.shiftCode;
    
    let results = this.shifts.filter(item => item.shiftCode === this.shiftCreateForm.value.shiftCode);
   
     if(results.length > 0 && !this.isEditMode){
       return true;
     }
     return false;
    
  }
  onShiftCodeChange():void{
     this.isShiftDuplicate = false;
  }


}
