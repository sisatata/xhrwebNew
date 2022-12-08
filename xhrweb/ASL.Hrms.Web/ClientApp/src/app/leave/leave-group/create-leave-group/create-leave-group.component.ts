import { Component, EventEmitter, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { LeaveGroup } from "src/app/shared/models";
import { CompanyService, LeaveGroupService } from "src/app/shared/services";
import { AlertService } from "src/app/shared/services/alert.service";
@Component({
  selector: "app-create-leave-group",
  templateUrl: "./create-leave-group.component.html",
  styleUrls: ["./create-leave-group.component.css"],
})
export class CreateLeaveGroupComponent implements OnInit {
  onLeaveGroupCreateEvent: EventEmitter<any> = new EventEmitter();
  onLeaveGroupEditEvent: EventEmitter<any> = new EventEmitter();
  leaveGroupCreateForm: FormGroup;
  submitted = false;
  employeeId: any;
  companies: any = [];
  leaveGroup: LeaveGroup;
  isEditMode = false;
  companyId: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private dialogRef: MatDialogRef<CreateLeaveGroupComponent>,
    private formBuilder: FormBuilder,
    private leaveGroupService: LeaveGroupService,
    private companyService: CompanyService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.leaveGroup = new LeaveGroup();
    if (isNaN(data)) {
      this.leaveGroup = new LeaveGroup(data);
      this.companyId = this.leaveGroup.companyId;
    }
    if (this.leaveGroup.id) {
      this.isEditMode = true;
    } else {
      this.isEditMode = false;
    }
    this.buildForm();
  }
  ngOnInit(): void {
    this.getAllCompanies();
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  onChangeCompany(): void {
    this.companyId = this.f.companyId.value;
  }
  buildForm(): void {
    this.leaveGroupCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      leaveTypeGroupName: [
        this.leaveGroup.leaveTypeGroupName,
        [Validators.required, Validators.maxLength(250)],
      ],
      leaveTypeGroupNameLC: [this.leaveGroup.leaveTypeGroupNameLC],
    });
  }
  onSubmit(): void {
    this.submitted = true;
    // stop here if form is invalid
    if (this.leaveGroupCreateForm.invalid) {
      return;
    }
    if (this.leaveGroup.id === undefined) {
      this.createLeaveGroup();
    } else {
      this.editLeaveGroup();
    }
  }
  createLeaveGroup(): void {
    this.leaveGroup = new LeaveGroup(this.leaveGroupCreateForm.value);
    this.loading = true;
    this.leaveGroupService.createLeaveGroup(this.leaveGroup).subscribe(
      (res) => {
        this.onLeaveGroupCreateEvent.emit(true);
        if ((res as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Leave Group added successfully");
          this.dialogRef.close();
        } else {
          this.isFormValid = false;
          this.errorMessages = (res as any).message;
        }
        this.loading = true;
      },
      () => {
        this.loading = false;
      }
    );
  }
  editLeaveGroup(): void {
    let newData = new LeaveGroup(this.leaveGroupCreateForm.value);
    this.leaveGroup.companyId = this.companyId;
    this.leaveGroup.leaveTypeGroupName = newData.leaveTypeGroupName;
    this.leaveGroup.leaveTypeGroupNameLC = newData.leaveTypeGroupNameLC;
    this.loading = true;
    this.leaveGroupService.editLeaveGroup(this.leaveGroup).subscribe(
      (res) => {
        this.onLeaveGroupEditEvent.emit(true);
        if ((res as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Leave Group added successfully");
          this.dialogRef.close();
        } else {
          this.isFormValid = false;
          this.errorMessages = (res as any).message;
        }
        this.loading = true;
      },
      () => {
        this.loading = false;
      }
    );
  }
  close(): void {
    this.dialogRef.close();
  }
  get f() {
    return this.leaveGroupCreateForm.controls;
  }
}
