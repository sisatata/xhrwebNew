import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Branch } from 'src/app/shared/models';
import { CompanyService, BranchService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-branch',
  templateUrl: './create-branch.component.html',
  styleUrls: ['./create-branch.component.css']
})
export class CreateBranchComponent implements OnInit {
  onBranchCreateEvent: EventEmitter<any> = new EventEmitter();
  onBranchEditEvent: EventEmitter<any> = new EventEmitter();
  branchCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  branch: Branch = new Branch();
  branchId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateBranchComponent>,
    private formBuilder: FormBuilder,
    private branchservice: BranchService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.branch = new Branch();
    if (isNaN(data)) {
      this.branch = new Branch(data);
      this.companyId = this.branch.companyId;
    } else {
    }
    this.buildForm();
    this.getAllCompanies();
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
    this.branchCreateForm = this.formBuilder.group({
      companyId: [this.branch.companyId],
      branchName: [this.branch.branchName, [Validators.required, Validators.maxLength(250)]],
      branchLocalizedName: [this.branch.branchLocalizedName, [Validators.maxLength(150)]],
      shortName: [this.branch.shortName, [Validators.required, Validators.maxLength(50)]],
      sortOrder: [this.branch.sortOrder],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.branchCreateForm.invalid) {
      return;
    }
    if (this.branch.id === undefined) {
      this.createBranch();
    }
    else {
      this.editBranch();
    }
  }
  createBranch() {
    this.branch = new Branch(this.branchCreateForm.value);
    this.loading = true;
    this.branchservice.createBranch(this.branch).subscribe((data: any) => {
      this.onBranchCreateEvent.emit(this.branch.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Branch added successfully");
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.alertService.error(error);
      this.loading = false;
    });
  }
  editBranch() {
    let newData = new Branch(this.branchCreateForm.value);
    if (this.branch !== null) {
      this.branch.branchName = newData.branchName;
      this.branch.branchLocalizedName = newData.branchLocalizedName;
      this.branch.shortName = newData.shortName;
      this.branch.companyId = newData.companyId;
      this.branch.sortOrder = newData.sortOrder;
      this.loading = true;
      this.branchservice.editBranch(this.branch).subscribe((data: any) => {
        this.onBranchEditEvent.emit(this.branch.id)
        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Branch updated successfully");
          this.dialogRef.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (data as any).message;
        }
        this.loading = false;
      }, (error: any) => {
        this.alertService.error(error);
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
  get f() { return this.branchCreateForm.controls; }
}
