import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LeaveGroup, LeaveType } from 'src/app/shared/models';
import { CompanyService, LeaveGroupService, LeaveTypeService } from 'src/app/shared/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-leave-type',
  templateUrl: './create-leave-type.component.html',
  styleUrls: ['./create-leave-type.component.css']
})
export class CreateLeaveTypeComponent implements OnInit {
  onLeaveTypeCreateEvent: EventEmitter<any> = new EventEmitter();
  onLeaveTypeEditEvent: EventEmitter<any> = new EventEmitter();
  leaveTypeCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  leaveType: LeaveType;
  leaveTypeId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  leaveGroups: LeaveGroup[] = [];
  leaveTypeGroupId: number;
  leaveGroupId:number;
  loading: boolean = false;
  test:any =1;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private leaveGroupService: LeaveGroupService,
    private dialogRef: MatDialogRef<CreateLeaveTypeComponent>,
    private formBuilder: FormBuilder,
    private leaveTypeService: LeaveTypeService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.leaveType = new LeaveType();
    if (isNaN(data)) {
      this.leaveType = new LeaveType(data);
      
      this.companyId = this.leaveType.companyId;
      this.leaveGroupId = this.leaveType.leaveTypeGroupId;
      this.buildForm();
    } else {
    }
    if(this.leaveType.id){
      this.isEditMode = true;
    }
    this.buildForm();
    this.getAllLeaveGroups();
  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllLeaveGroups();
    this.buildForm();
   
  }

  getAllLeaveGroups(): void {
    this.loading = true;
    this.leaveGroupService.getAllLeaveGroups(this.companyId).subscribe(
      (res) => {
        this.loading = false;
        this.leaveGroups = res;
        
      },
      () => {
        this.loading = false;
        this.leaveGroups = [];
      }
    );
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
    this.leaveTypeCreateForm = this.formBuilder.group({
      companyId: [this.leaveType.companyId],
      leaveTypeName: [this.leaveType.leaveTypeName, [Validators.required, Validators.maxLength(250)]],
      leaveTypeLocalizedName: [this.leaveType.leaveTypeLocalizedName, [Validators.maxLength(150)]],
      leaveTypeShortName: [this.leaveType.leaveTypeShortName, [Validators.required, Validators.maxLength(50)]],
      balance: [this.leaveType.balance, [Validators.required]],
      isCarryForward: [this.leaveType.isCarryForward],
      isFemaleSpecific: [this.leaveType.isFemaleSpecific],
      isPaid: [this.leaveType.isPaid],
      isEncashable: [this.leaveType.isEncashable],
      encashReserveBalance: [this.leaveType.encashReserveBalance],
      isDependOnDateOfConfirmation: [this.leaveType.isDependOnDateOfConfirmation],
      isLeaveDaysFixed: [this.leaveType.isLeaveDaysFixed],
      isBasedWorkingDays: [this.leaveType.isBasedWorkingDays],
      consecutiveLimit: [this.leaveType.consecutiveLimit],
      isAllowAdvanceLeaveApply: [this.leaveType.isAllowAdvanceLeaveApply],
      isAllowWithPrecedingHoliday: [this.leaveType.isAllowWithPrecedingHoliday],
      isAllowOpeningBalance: [this.leaveType.isAllowOpeningBalance],
      isPreventPostLeaveApply: [this.leaveType.isPreventPostLeaveApply],
      leaveTypeGroupId: [this.leaveType.leaveTypeGroupId]
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.leaveTypeCreateForm.invalid) {
      return;
    }
    if (this.leaveType.id === undefined) {
      this.createLeaveType();
    } else {
      this.editLeaveType();
    }
  }
  createLeaveType() {
    this.leaveType = new LeaveType(this.leaveTypeCreateForm.value);
    this.loading = true;
    this.leaveType.leaveTypeGroupId = this.leaveGroupId;
    this.leaveTypeService.createLeaveType(this.leaveType).subscribe((data: any) => {
      this.onLeaveTypeCreateEvent.emit(this.leaveType.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Leave Type added successfully");
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
    });
  }
  editLeaveType() {
    let changeData = new LeaveType(this.leaveTypeCreateForm.value);
    if (this.leaveType !== null) {
      this.leaveType.leaveTypeName = changeData.leaveTypeName;
      this.leaveType.leaveTypeLocalizedName = changeData.leaveTypeLocalizedName;
      this.leaveType.leaveTypeShortName = changeData.leaveTypeShortName;
      this.leaveType.balance = changeData.balance;
      this.leaveType.isCarryForward = changeData.isCarryForward;
      this.leaveType.isFemaleSpecific = changeData.isFemaleSpecific;
      this.leaveType.isPaid = changeData.isPaid;
      this.leaveType.isEncashable = changeData.isEncashable;
      this.leaveType.encashReserveBalance = changeData.encashReserveBalance;
      this.leaveType.isDependOnDateOfConfirmation = changeData.isDependOnDateOfConfirmation;
      this.leaveType.isLeaveDaysFixed = changeData.isLeaveDaysFixed;
      this.leaveType.isBasedWorkingDays = changeData.isBasedWorkingDays;
      this.leaveType.consecutiveLimit = changeData.consecutiveLimit;
      this.leaveType.isAllowAdvanceLeaveApply = changeData.isAllowAdvanceLeaveApply;
      this.leaveType.isAllowWithPrecedingHoliday = changeData.isAllowWithPrecedingHoliday;
      this.leaveType.isAllowOpeningBalance = changeData.isAllowOpeningBalance;
      this.leaveType.isPreventPostLeaveApply = changeData.isPreventPostLeaveApply;
      this.leaveType.companyId = changeData.companyId;
      this.leaveType.leaveTypeGroupId = changeData.leaveTypeGroupId;
      this.leaveType.leaveTypeGroupId = this.leaveGroupId;
      this.loading = true;
      this.leaveTypeService.editLeaveType(this.leaveType).subscribe((data: any) => {
        this.onLeaveTypeEditEvent.emit(this.leaveType.id)
        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Leave Type Updated successfully");
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
  get f() { return this.leaveTypeCreateForm.controls; }
}
