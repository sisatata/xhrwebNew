import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { BenefitDeductionCode, BillClaim } from 'src/app/shared/models';
import { BenefitBillClaimService, BenefitDeductionService, FileUploadService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-bill-claim',
  templateUrl: './create-bill-claim.component.html',
  styleUrls: ['./create-bill-claim.component.css']
})
export class CreateBillClaimComponent extends BaseAuthorizedComponent implements OnInit {
  onBillCreateEvent: EventEmitter<any> = new EventEmitter();
  onBillEditEvent: EventEmitter<any> = new EventEmitter();
  billCreateForm: FormGroup;
  errorMessages: any[] = [];
  errorMessage: string;
  submitted = false;
  billClaim: BillClaim;
  isEditMode = false;
  branchId: any;
  departmentId: any;
  positionId: any;
  companies: any;
  departments: any;
  branches: any;
  loading: boolean = false;
  companyId: any;
  employeeId: string;
  fileName: any;
  uploadedFiles: any;
  fileToUpload: File;
  file: any;
  positions: any;
  fileValidationError: any;
  genders: any;
  nationalities: any;
  hidePassword: boolean = true;
  hideConfirmPassword: boolean = true;
  isFormValid: boolean;
  benefitDeductions: BenefitDeductionCode[];
  imagePreviewPath: any;
  constructor(private injector: Injector,
    private dialogRef: MatDialogRef<CreateBillClaimComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private benefitBillClaimService: BenefitBillClaimService,
    private benefitDeductionService: BenefitDeductionService,
    private datePipe: DatePipe,
    private fileUploadService: FileUploadService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    super(injector);
    this.billClaim = new BillClaim(data);
    this.isEditMode = this.billClaim.id ? true : false;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.companyId = this.companyId ? this.authService.getLoggedInUserInfo().companyId : this.companyId;
  }
  ngOnInit(): void {
    this.getAllBenefitDeductionsByCompanyId();
    this.buildForm();
  }
  buildForm(): void {
    this.billCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      employeeId: [this.employeeId, [Validators.required]],
      benefitDeductionId: [this.billClaim.benefitDeductionId, [Validators.required]],
      claimDate: [this.billClaim.claimDate, [Validators.required]],
      claimAmount: [this.billClaim.claimAmount, [Validators.required]],
      remarks: [this.billClaim.remarks, [Validators.required]],
    });
  }
  getAllBenefitDeductionsByCompanyId(): void {
    this.loading = true;
    this.benefitDeductionService.getAllBenifitDeductionByCompanyId(this.companyId).subscribe(result => {
      this.benefitDeductions = result;
      this.totalItems = result.length;
      this.loading = false;
    }, () => { this.loading = false; })
  }
  uploadFile(files) {
    this.fileValidationError = null;
    let fileInfo = this.fileUploadService.imageFileUpload(files);
    if (fileInfo.validationError != null) {
      this.fileValidationError = fileInfo.validationError;
      return;
    }
    this.fileName = fileInfo.fileName;
    this.uploadedFiles = fileInfo.formData;
    this.fileToUpload = <File>files[0];
    var formData = new FormData();
    formData.append('file', this.fileToUpload, this.fileToUpload.name);
    formData.append('id', this.companyId);
    var reader = new FileReader();
  //  console.log(this.fileToUpload)
    reader.onload = (event: any) => {
      this.imagePreviewPath = event.target.result;
    }
    reader.readAsDataURL(files[0]);
  }
  get f() { return this.billCreateForm.controls; }
  onSubmit(): void {
    this.submitted = true;
    if (this.billCreateForm.invalid) {
      return;
    }
    if (this.billClaim.id === null || this.billClaim.id === undefined) {
      this.createBillClaim();
    }
    else {
      this.editBillClaim();
    }
  }
  close(): void {
    this.dialogRef.close();
  }
  createBillClaim(): void {
    this.billClaim = new BillClaim(this.billCreateForm.value);
    const formData = new FormData();
    this.billClaim.claimDate = this.datePipe.transform(new Date(this.billClaim.claimDate), 'yyyy-MM-dd');
    formData.append("employeeId", this.billClaim.employeeId);
    formData.append("companyId", this.billClaim.companyId);
    formData.append("benefitDeductionId", this.billClaim.benefitDeductionId);
    formData.append("claimDate", this.billClaim.claimDate);
    formData.append("billDate", this.billClaim.claimDate);
    formData.append("claimAmount", this.billClaim.claimAmount.toString());
    formData.append("remarks", this.billClaim.remarks);
    if (this.fileToUpload) {
      formData.append("billAttachment", this.billClaim.billAttachment);
      formData.append("file", this.fileToUpload, this.fileToUpload.name);
    }
    // other fields
    this.loading = true;
    this.benefitBillClaimService.createBillClaim(formData).subscribe(res => {
      if ((res as any).status) {
        this.isFormValid = true;
        this.alertService.success('Bill created successfully');
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  editBillClaim(): void {
    let newData = new BillClaim(this.billCreateForm.value);
    const formData = new FormData();
    newData.claimDate = this.datePipe.transform(new Date(newData.claimDate), 'yyyy-MM-dd');
    formData.append("employeeId", this.billClaim.employeeId);
    formData.append("companyId", this.billClaim.companyId);
    formData.append("benefitDeductionId", newData.benefitDeductionId);
    formData.append("claimDate", newData.claimDate);
    formData.append("billDate", newData.claimDate);
    formData.append("claimAmount", newData.claimAmount.toString());
    formData.append("remarks", newData.remarks);
    formData.append("id", this.billClaim.id);
    if (this.fileToUpload) {
      formData.append("billAttachment", newData.billAttachment);
      formData.append("file", this.fileToUpload, this.fileToUpload.name);
    }
    // other fields
    this.loading = true;
    this.benefitBillClaimService.updateBillClaim(formData).subscribe(res => {
      if ((res as any).status) {
        this.isFormValid = true;
        this.onBillEditEvent.emit(true);
        this.alertService.success('Bill created edited');
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
}
