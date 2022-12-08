import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Company } from 'src/app/shared/models';
import { CompanyService, FileUploadService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
  selector: 'app-company-detail',
  templateUrl: './company-detail.component.html',
  styleUrls: ['./company-detail.component.css']
})
export class CompanyDetailComponent implements OnInit {
  @BlockUI('company-detail-section') blockUI: NgBlockUI;
  @Input() companyId: any;
  @Input() companyName: any;
  @Input() companySlogan: any;
  @Input() companyShortName: any;
  @Input() isActive: any;
  @Input() companyWebsite: any;
  @Input() facebookLink: any;
  @Input()  companyLogo:any;
  panelOpenState = false;
  companyDetailForm: FormGroup;
  submitted: boolean = false;
  company: Company = new Company();
  isFormValid: boolean = true;
  errorMessages: any;
  loading: boolean = false;
  shortName: any;
  fileValidationError: any;
  fileName: any;
  uploadedFiles: any;
  fileToUpload: File;
  imagePreviewPath: any;
  companyImage:string ='assets/images/avatar.png';
  constructor(private formBuilder: FormBuilder, private companyService: CompanyService,
    private alertService: AlertService,
    private fileUploadService: FileUploadService,
   
    ) { 

      
    }
  
  ngOnInit(): void {
    this.companyImage = this.companyLogo;
      this.companySlogan = this.companySlogan === "null"? '': this.companySlogan;
      this.companyWebsite = this.companyWebsite === null? '': this.companyWebsite;
      this.facebookLink = this.facebookLink === null? '': this.facebookLink;
      this.buildForm();

  }
  get f() { return this.companyDetailForm.controls; }

  buildForm(): void {
    this.companyDetailForm = this.formBuilder.group({
      id: [this.companyId, Validators.required],
      companyName: [this.companyName, [Validators.required, Validators.maxLength(250)]],
      companySlogan: [this.companySlogan, [Validators.maxLength(250)]],
      shortName: [this.companyShortName, [Validators.maxLength(250)]],
      companyWebsite:[this.companyWebsite, [Validators.maxLength(250)]],
      facebookLink:[this.facebookLink, [Validators.maxLength(250)]],
      isActive: [this.isActive]
    });

  }
  saveCompanyDetail(): void {
    this.submitted = true;

    if (this.companyDetailForm.invalid) {
      return;
    }
    else {
      this.editCompanyDetails();
    }
  }
  editCompanyDetails(): void {
    this.loading = true;
    this.blockUI.start();
    this.company = new Company(this.companyDetailForm.value);
    //console.log(this.company);
    this.companyService.editCompany(this.company).subscribe(result => {
      this.loading = false;
      if((result as any).status === true){
        this.isFormValid =true;
        this.alertService.success("Company detail edited successfully");
        
      }
      else{
          this.isFormValid = false;
          this.errorMessages = (result as any).message;
      }
      this.blockUI.stop();

    },()=>{
      this.blockUI.stop();
      this.loading = false;
    })

  }
  uploadImage(files) {
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
    reader.onload = (event: any) => {
      this.imagePreviewPath = event.target.result;
    }
    reader.readAsDataURL(files[0]);
    this.loading = true;
  // console.log(formData);
    this.companyService.uploadCompanymage(formData).subscribe(result => {
      if (result && (result as any).status == true) {
        this.company.id = (result as any).id;
        this.imagePreviewPath = (result as any).pictureUri;
        this.loading = false;
        this.alertService.success("Company logo Saved Successfully");
        this.getCompanyById();
    
        
      }
      else {
        this.alertService.error((result as any).message);
        this.loading = false;
      }
    }, () => {
      this.loading = false;
    });
  }
  getCompanyById():void{
    this.companyService.getCompanyById(this.companyId).subscribe(result=>{
       this.companyImage =( result as any).companyLogoUri;
      // console.log(this.companyImage)
    },()=>{})
  }
}
