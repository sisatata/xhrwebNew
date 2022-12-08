import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Holiday } from 'src/app/shared/models';
import { HolidayService, CommonLookUpTypeService, CompanyService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-create-holiday',
  templateUrl: './create-holiday.component.html',
  styleUrls: ['./create-holiday.component.css']
})
export class CreateHolidayComponent implements OnInit {
  onHolidayCreateEvent: EventEmitter<any> = new EventEmitter();
  onHolidayEditEvent: EventEmitter<any> = new EventEmitter();
  holidayCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  holiday: Holiday;
  isEditMode = false;
  companyId: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  startDate: any;
  endDate: any;
  constructor(
    private dialogRef: MatDialogRef<CreateHolidayComponent>,
    private formBuilder: FormBuilder,
    private holidayservice: HolidayService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private companyService: CompanyService,
    private alertService: AlertService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data) {
    this.holiday = new Holiday();
    if (isNaN(data)) {
      this.holiday = new Holiday(data);
      this.companyId = this.holiday.companyId;
    }
    if (this.holiday.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
  }
  ngOnInit() {
    this.getAllCompanies();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }
  buildForm() {
    this.holidayCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      // holidayDate: [this.holiday.holidayDate, [Validators.required]],
      startDate: [this.holiday.startDate, [Validators.required]],
      endDate: [this.holiday.endDate, [Validators.required]],
      reason: [this.holiday.reason, [Validators.required, Validators.maxLength(150)]]
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.holidayCreateForm.invalid) {
      return;
    }
    if (this.holiday.id === undefined) {
      this.createHoliday();
    }
    else {
      this.editHoliday();
    }
    //this.dialogRef.close();
  }
  createHoliday() {
    this.holiday = new Holiday(this.holidayCreateForm.value);
    // this.holiday.holidayDate = this.datePipe.transform(new Date(this.holiday.holidayDate), 'yyyy/MM/dd');
    this.holiday.startDate = this.datePipe.transform(new Date(this.holiday.startDate), 'yyyy/MM/dd');
    this.holiday.endDate = this.datePipe.transform(new Date(this.holiday.endDate), 'yyyy/MM/dd');
    this.holiday.companyId = this.companyId;
    this.loading = true;
    this.holidayservice.createHoliday(this.holiday).subscribe((data: any) => {
      this.onHolidayCreateEvent.emit(this.holiday.companyId);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Holiday added successfully");
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }
  editHoliday() {
    let newData = new Holiday(this.holidayCreateForm.value);

  
    if (this.holiday !== null) {
      this.holiday.companyId = newData.companyId;
      // this.holiday.holidayDate = this.datePipe.transform(new Date(newData.holidayDate), 'yyyy/MM/dd');
      this.holiday.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy/MM/dd');
    this.holiday.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy/MM/dd');
      this.holiday.reason = newData.reason;
      this.loading = true;
      this.holidayservice.editHoliday(this.holiday).subscribe((data: any) => {
        this.onHolidayEditEvent.emit(this.holiday.id)
        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Holiday edited successfully");
          this.dialogRef.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (data as any).message;
        }
        this.loading = false;
      }, (error: any) => {
        this.showErrorMessage(error);
        this.loading = false;
      });
    }
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }

  updateCalcs(data:any):void{
   
    this.holidayCreateForm.controls['startDate'].setValue(data.value);
    this.holidayCreateForm.controls['endDate'].setValue(data.value);

  }
  get f() { return this.holidayCreateForm.controls; }
}
