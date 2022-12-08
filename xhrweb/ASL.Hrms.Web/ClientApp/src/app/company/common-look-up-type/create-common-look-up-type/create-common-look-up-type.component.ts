import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonLookUpType, Company, Guid } from 'src/app/shared/models';
import { CommonLookUpTypeService, CompanyService } from 'src/app/shared/services';
import { AuthService } from 'src/app/auth/services/auth.service';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-common-look-up-type',
  templateUrl: './create-common-look-up-type.component.html',
  styleUrls: ['./create-common-look-up-type.component.css']
})
export class CreateCommonLookUpTypeComponent implements OnInit {
  onCommonLookUpTypeCreateEvent: EventEmitter<any> = new EventEmitter();
  onCommonLookUpTypeEditEvent: EventEmitter<any> = new EventEmitter();
  commonLookUpTypeCreateForm: FormGroup
  submitted = false;
  parentId: any;
  commonLookUpType: CommonLookUpType;
  isEditMode = false;
  companies: Company[] = [];
  commonLookUpTypes: CommonLookUpType[] = [];
  companyId: any = Guid.empty;;
  isSystemAdmin: boolean = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  isCompanySelected: boolean = true;
  isAllCompanies:any = true;
  isSystemAdmin: boolean= false;
  constructor(
    private dialogRef: MatDialogRef<CreateCommonLookUpTypeComponent>,
    private formBuilder: FormBuilder,
    private commonLookUpTypeservice: CommonLookUpTypeService,
    private companyService: CompanyService,
    private authService: AuthService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.commonLookUpType = new CommonLookUpType();
    if (isNaN(data)) {
      this.commonLookUpType = new CommonLookUpType(data);
      this.companyId = this.commonLookUpType.companyId;
      this.parentId = this.commonLookUpType.parentId;
     
    }
    if (this.commonLookUpType.id) {
      this.isEditMode = true;
     
      //this.isAllCompanies = this.companyId === Guid.empty? true : this.companyId===null ? true : this.companyId === undefined? true : false;
    }
    else {
      this.isEditMode = false;
    }
    if (this.commonLookUpType.companyId) {
      this.getCommonLookUpTypesByCompanyId();
    }
    this.buildForm();
    this.isSystemAdmin = this.authService.isSystemAdmin;
  
  }
  ngOnInit() {
    this.getAllCompanies();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe(result => { this.companies = result; });
  }
  getCommonLookUpTypesByCompanyId() {
    if (this.commonLookUpType.companyId) {
      this.commonLookUpTypeservice.getAllCommonLookUpTypeByCompanyId(this.commonLookUpType.companyId).subscribe(result => 
        { this.commonLookUpTypes = result;
         
        
        });
    }
  }
  buildForm() {
    this.commonLookUpTypeCreateForm = this.formBuilder.group({
      companyId: [this.commonLookUpType.companyId],
      parentId: [this.commonLookUpType.parentId],
      shortCode: [this.commonLookUpType.shortCode, [Validators.required, Validators.maxLength(20)]],
      lookUpTypeName: [this.commonLookUpType.lookUpTypeName, [Validators.required, Validators.maxLength(50)]],
      lookUpTypeNameLC: [this.commonLookUpType.lookUpTypeNameLC, [Validators.maxLength(50)]],
      remarks: [this.commonLookUpType.remarks, [Validators.maxLength(250)]],
      sortOrder: [this.commonLookUpType.sortOrder, [Validators.required]]
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.commonLookUpTypeCreateForm.invalid) {
      return;
    }
    if (this.commonLookUpType.id === undefined) {
      this.createCommonLookUpType();
    }
    else {
      this.editCommonLookUpType();
    }
    //this.dialogRef.close();
  }
  createCommonLookUpType() {
    this.commonLookUpType = new CommonLookUpType(this.commonLookUpTypeCreateForm.value);
    this.loading = true;
   // this.commonLookUpType.companyId = this.isAllCompanies === true ? Guid.empty: this.companyId;
    this.commonLookUpTypeservice.createCommonLookUpType(this.commonLookUpType).subscribe((data: any) => {
      this.onCommonLookUpTypeCreateEvent.emit();
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Common Look Up Type added successfully");
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
      //this.alertService.success("CommonLookUpType added successfully");
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }
  editCommonLookUpType() {
    let newData = new CommonLookUpType(this.commonLookUpTypeCreateForm.value);

    if (this.commonLookUpType !== null) {
      this.commonLookUpType.companyId = newData.companyId;
      this.commonLookUpType.shortCode = newData.shortCode;
      this.commonLookUpType.lookUpTypeName = newData.lookUpTypeName;
      this.commonLookUpType.lookUpTypeNameLC = newData.lookUpTypeNameLC;
      this.commonLookUpType.remarks = newData.remarks;
      this.commonLookUpType.sortOrder = newData.sortOrder;
      this.loading = true;
      //this.commonLookUpType.companyId = this.isAllCompanies === true ? Guid.empty: this.companyId;
      this.commonLookUpTypeservice.editCommonLookUpType(this.commonLookUpType).subscribe((data: any) => {
        this.onCommonLookUpTypeEditEvent.emit(this.commonLookUpType.id)

        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Common Look Up Type edited successfully");
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
  get f() { return this.commonLookUpTypeCreateForm.controls; }
  change(data:any):void{
   
    this.isAllCompanies = data;
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    
  }
}
