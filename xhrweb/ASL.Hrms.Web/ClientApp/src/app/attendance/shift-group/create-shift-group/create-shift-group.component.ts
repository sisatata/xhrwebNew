import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ShiftGroup } from 'src/app/shared/models';
import { ShiftGroupService, CommonLookUpTypeService, CompanyService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';

@Component({
  selector: 'app-create-shift-group',
  templateUrl: './create-shift-group.component.html',
  styleUrls: ['./create-shift-group.component.css']
})

export class CreateShiftGroupComponent implements OnInit {
  onShiftGroupCreateEvent: EventEmitter<any> = new EventEmitter();
  onShiftGroupEditEvent: EventEmitter<any> = new EventEmitter();

  shiftGroupCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  
  shiftGroup: ShiftGroup;
  isEditMode = false;
  companyId: any;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(
    private dialogRef: MatDialogRef<CreateShiftGroupComponent>,
    private formBuilder: FormBuilder,
    private shiftGroupservice: ShiftGroupService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private companyService: CompanyService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.shiftGroup = new ShiftGroup();
    if (isNaN(data)) {
      this.shiftGroup = new ShiftGroup(data);
      this.companyId = this.shiftGroup.companyId;
    }

    if (this.shiftGroup.id) {
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

  //getAllPhoneTypes() {
  //  if (this.companyId) {
  //    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "PhoneType").subscribe(result => {
  //      this.phoneTypes = result
  //    });
  //  }
  //}

  buildForm() {
    this.shiftGroupCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      shiftGroupName: [this.shiftGroup.shiftGroupName, [Validators.required, Validators.maxLength(150)]],
      shiftGroupNameLC: [this.shiftGroup.shiftGroupNameLC]
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.shiftGroupCreateForm.invalid) {
      return;
    }
    if (this.shiftGroup.id === undefined) {
      this.createShiftGroup();
    }
    else {
      this.editShiftGroup();
    }
   // this.dialogRef.close();
  }

  createShiftGroup() {
    this.shiftGroup = new ShiftGroup(this.shiftGroupCreateForm.value);

    this.shiftGroup.companyId = this.companyId;
    this.loading = true;
    this.shiftGroupservice.createShiftGroup(this.shiftGroup).subscribe((data: any) => {
      this.onShiftGroupCreateEvent.emit(this.shiftGroup.companyId);
      if((data as any).status === true){
        this.isFormValid = true;
        this.alertService.success("Shift Group added successfully");

        this.dialogRef.close();

      }
      else{
        this.isFormValid = false;
        this.errorMessages = (data as  any).message;
      }
     this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
    });
  }


  editShiftGroup() {
    let newData = new ShiftGroup(this.shiftGroupCreateForm.value);
    if (this.shiftGroup !== null) {
      this.shiftGroup.shiftGroupName = newData.shiftGroupName;
      this.shiftGroup.shiftGroupNameLC = newData.shiftGroupNameLC;
     this.loading = true;
      this.shiftGroupservice.editShiftGroup(this.shiftGroup).subscribe((data: any) => {
        this.onShiftGroupEditEvent.emit(this.shiftGroup.id);
        if((data as any).status === true){
          this.isFormValid = true;
          this.alertService.success("Shift Group edited successfully");

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

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.shiftGroupCreateForm.controls; }
}


