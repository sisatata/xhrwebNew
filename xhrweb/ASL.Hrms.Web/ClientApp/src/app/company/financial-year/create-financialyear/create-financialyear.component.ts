import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FinancialYear } from 'src/app/shared/models';
import { CompanyService, FinancialYearService } from 'src/app/shared/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from 'src/app/shared/services/alert.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-financialyear',
  templateUrl: './create-financialyear.component.html',
  styleUrls: ['./create-financialyear.component.css']
})
export class CreateFinancialyearComponent implements OnInit {

  onFinancialYearCreateEvent: EventEmitter<any> = new EventEmitter();
  onFinancialYearEditEvent: EventEmitter<any> = new EventEmitter();
  financialYearCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  financialYear: FinancialYear;
  financialYearId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateFinancialyearComponent>,
    private formBuilder: FormBuilder,
    private financialYearservice: FinancialYearService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data

  ) {
    this.financialYear = new FinancialYear();
    if (isNaN(data)) {
      this.financialYear = new FinancialYear(data);
      this.companyId = this.financialYear.companyId;

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
    this.financialYearCreateForm = this.formBuilder.group({
      companyId: [this.financialYear.companyId],
      financialYearName: [this.financialYear.financialYearName, [Validators.required, Validators.maxLength(250)]],
      financialYearLocalizedName: [this.financialYear.financialYearLocalizedName, [Validators.maxLength(150)]],
      financialYearStartDate: [this.financialYear.financialYearStartDate, [Validators.required]],
      financialYearEndDate: [this.financialYear.financialYearEndDate, [Validators.required]],
      isRunningYear: [this.financialYear.isRunningYear],
      sortOrder: [this.financialYear.sortOrder, [Validators.required, Validators.min(1)]],
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.financialYearCreateForm.invalid) {
      return;
    }
    if (this.financialYear.id === undefined) {
      this.createFinancialYear();
    }
    else {
      this.editFinancialYear();
    }
   
  }

  createFinancialYear() {

    this.financialYear = new FinancialYear(this.financialYearCreateForm.value);
    this.financialYear.financialYearStartDate = this.datePipe.transform(new Date(this.financialYear.financialYearStartDate), 'yyyy/MM/dd');
    this.financialYear.financialYearEndDate = this.datePipe.transform(new Date(this.financialYear.financialYearEndDate), 'yyyy/MM/dd');
    this.loading = true;
    
    this.financialYearservice.createFinancialYear(this.financialYear).subscribe((data: any) => {
      this.onFinancialYearCreateEvent.emit(this.financialYear.id);
      if((data as any).status === true){
        this.alertService.success("Financial Year added successfully");
        this.isFormValid = true;
        this.dialogRef.close();
        }
        else{
          this.isFormValid = false;
          this.errorMessages = (data as any).message;
        }
        this.loading = false;
      
      
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });

  }
  editFinancialYear() {
    let changeData = new FinancialYear(this.financialYearCreateForm.value);
    if (this.financialYear !== null) {
      this.financialYear.financialYearName = changeData.financialYearName;
      this.financialYear.financialYearLocalizedName = changeData.financialYearLocalizedName;
      this.financialYear.financialYearStartDate = this.datePipe.transform(new Date(changeData.financialYearStartDate), 'yyyy/MM/dd');
      this.financialYear.financialYearEndDate = this.datePipe.transform(new Date(changeData.financialYearEndDate), 'yyyy/MM/dd');
      this.financialYear.isRunningYear = changeData.isRunningYear;
      this.financialYear.sortOrder = changeData.sortOrder;
      this.financialYear.companyId = changeData.companyId;
      this.loading = true;
      this.financialYearservice.editFinancialYear(this.financialYear).subscribe((data: any) => {
        this.onFinancialYearEditEvent.emit(this.financialYearId)
        if((data as any).status === true){
        this.alertService.success("Financial Year updated successfully");
        this.isFormValid = true;
        this.dialogRef.close();

        }
        else{
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
  get f() { return this.financialYearCreateForm.controls; }

}
