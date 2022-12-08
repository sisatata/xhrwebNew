import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IncomeTaxParameter, IncomeTaxPayerType } from 'src/app/shared/models';
import { CompanyService, IncomeTaxParameterService, IncomeTaxPayerTypeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-income-tax-parameter',
  templateUrl: './create-income-tax-parameter.component.html',
})
export class CreateIncomeTaxParameterComponent implements OnInit {
  onIncomeTaxParameterCreateEvent: EventEmitter<any> = new EventEmitter();
  onIncomeTaxParameterEditEvent: EventEmitter<any> = new EventEmitter();
  incomeTaxParameterCreateForm: FormGroup;
  submitted: boolean;
  incomeTaxPayerType: IncomeTaxPayerType;
  incomeTaxParameter: IncomeTaxParameter;
  isEditMode: boolean;
  isFormValid: boolean;
  incomeTaxPayerTypes: any[] = [];
  errorMessages: any;
  loading: boolean;
  companies: any;
  incomeTaxParameters: IncomeTaxParameter[];
  isDateOverLap: boolean;
  dateOverLapErrorMessage:string= 'Dates overlapping for parameter name';
  parameterId:any;
  constructor(
    private injector: Injector,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateIncomeTaxParameterComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private incomeTaxPayerTypeService: IncomeTaxPayerTypeService,
    private incomeTaxParameterService: IncomeTaxParameterService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data) {
    this.incomeTaxParameter = new IncomeTaxParameter();
    if (isNaN(data)) {
      this.incomeTaxParameter = new IncomeTaxParameter(data);
    }
    this.parameterId = this.incomeTaxParameter.id;
    if (this.incomeTaxParameter.id) {
      this.isEditMode = true;
      this.buildForm();
      this.checkDateOverLapsInEditMode();
    }
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllIncomeTaxPayerType();
     this.getAllIncomeTaxParameters();
  }
  buildForm(): void {
    this.incomeTaxParameterCreateForm = this.formBuilder.group({
      companyId: [this.incomeTaxParameter.companyId],
      parameterName: [this.incomeTaxParameter.parameterName, [Validators.required, Validators.maxLength(50)]],
      limitAmount: [this.incomeTaxParameter.limitAmount, [Validators.required]],
      limitPercentageOfBasic: [this.incomeTaxParameter.limitPercentageOfBasic, [Validators.required]],
      remarks: [this.incomeTaxParameter.remarks, [Validators.maxLength(50)]],
      payerTypeCode: [this.incomeTaxParameter.payerTypeCode],
      startDate: [this.incomeTaxParameter.startDate, [Validators.required]],
      endDate: [this.incomeTaxParameter.endDate, [Validators.required]],
      isActive: [this.incomeTaxParameter.isActive],
    });
  }
  get f() { return this.incomeTaxParameterCreateForm.controls; }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
      this.loading = false;
    })
  }
  getAllIncomeTaxPayerType(): void {
    //this.loading = true;
    this.incomeTaxPayerTypeService.getAllIncomeTaxPayerType(this.incomeTaxParameter.companyId).subscribe(result => {
      this.incomeTaxPayerTypes = result;
      // for setting as none
      this.incomeTaxPayerTypes.unshift({ id: 1, payerTypeName: 'None', payerTypeCode: 'None' });
    }, () => {
      this.loading = false;
    })
  }
  onSubmit(): void {
    this.submitted = true;
    if(this.isEditMode){
      if(!this.checkDateOverLapsInEditMode()){
        this.editIncomTaxParameter();
      }
      else{
       
        if (this.checkDuplicates()) {
          return;
        }
      }
      
        
      
      
    }
    
    if ( !this.isEditMode && this.checkDuplicates() ) {
      return;
    }
    if (this.incomeTaxParameterCreateForm.invalid) {
      return;
    }
    if (this.incomeTaxParameter.id === null || this.incomeTaxParameter.id === undefined) {
      this.createIncomTaxParameter();
    }
    else {
      this.editIncomTaxParameter();
    }
  }
  close(): void {
    this.dialogRef.close();
  }
  onChangeIncomeTaxPayerType(data: any): void {
    this.incomeTaxParameter.payerTypeCode = data.value;
  }
  createIncomTaxParameter(): void {
    this.incomeTaxParameter = new IncomeTaxParameter(this.incomeTaxParameterCreateForm.value);
    this.incomeTaxParameter.payerTypeCode = this.incomeTaxParameter.payerTypeCode === 'None' ? '' : this.incomeTaxParameter.payerTypeCode;
    this.incomeTaxParameter.startDate = this.datePipe.transform(new Date(this.incomeTaxParameter.startDate), 'yyyy-MM-dd');
    this.incomeTaxParameter.endDate = this.datePipe.transform(new Date(this.incomeTaxParameter.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.incomeTaxParameterService.createIncomeTaxParameter(this.incomeTaxParameter).subscribe(result => {
      this.onIncomeTaxParameterCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Income tax paremeter successfully created');
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  editIncomTaxParameter(): void {
    let newData = new IncomeTaxParameter(this.incomeTaxParameterCreateForm.value);
    if (this.incomeTaxParameter !== null) {
      this.incomeTaxParameter.isActive = newData.isActive;
      this.incomeTaxParameter.limitAmount = newData.limitAmount;
      this.incomeTaxParameter.limitPercentageOfBasic = newData.limitPercentageOfBasic;
      this.incomeTaxParameter.parameterName = newData.parameterName;
      this.incomeTaxParameter.payerTypeCode = newData.payerTypeCode;
      this.incomeTaxParameter.remarks = newData.remarks;
      this.incomeTaxParameter.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.incomeTaxParameter.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.loading = true;
      this.incomeTaxParameterService.editIncomeTaxParameter(this.incomeTaxParameter).subscribe(result => {
        this.onIncomeTaxParameterEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Income tax paremeter successfully updated');
          this.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (result as any).message;
        }
        this.loading = false;
      }, () => {
        this.loading = false
      })
    }
  }
  getAllIncomeTaxParameters(): void {
    
    this.incomeTaxParameterService.getAllIncomeTaxParameter(this.incomeTaxParameter.companyId).subscribe(result => {
      this.incomeTaxParameters = result;
      
      
      //this.incomeTaxPayerTypes.unshift({ id: 1, payerTypeName: 'All', payerTypeCode: 'All' });
    }, () => {
     
    })
  }
  checkDuplicates():boolean {
    let isDateOverLapped = this.checktDateOverLap();
    if(isDateOverLapped)  this.isDateOverLap = true;
    return isDateOverLapped; 

  }
  checktEndDateOverLap(startDate: any, endDate: any): boolean {

    let edDate = this.datePipe.transform(new Date(endDate), 'yyyy-MM-dd');
    let stDate = this.datePipe.transform(new Date(startDate), 'yyyy-MM-dd');
    if (new Date(edDate).getTime() >= new Date(stDate).getTime()) {

      return true;
    }
    return false;
  }
  checktDateOverLap(): boolean {

    let results = this.incomeTaxParameters.filter(item => item.parameterName === this.incomeTaxParameterCreateForm.value.parameterName);
    let startDate = this.datePipe.transform(new Date(this.incomeTaxParameterCreateForm.value.startDate), 'yyyy-MM-dd');
    let endDate = this.datePipe.transform(new Date(this.incomeTaxParameterCreateForm.value.endDate), 'yyyy-MM-dd');
    let dateOverLapped = false;
    results.find(item => {
      let edDate = this.datePipe.transform(new Date(item.endDate), 'yyyy-MM-dd');
      if (new Date(edDate).getTime() >= new Date(startDate).getTime()) {

        if (this.checktEndDateOverLap(item.startDate, endDate)) {
            if(this.incomeTaxParameter.id !== item.id){
          dateOverLapped = true;
          return dateOverLapped;
            }

        }



      }
    });

    return dateOverLapped;

  }
  checkDateOverLapsInEditMode():boolean{
    let editedParameterName = this.incomeTaxParameterCreateForm.value.slabName;
    let editedStartDate =  this.datePipe.transform(new Date(this.incomeTaxParameterCreateForm.value.startDate), 'yyyy-MM-dd');
    let editedEndDate = this.datePipe.transform(new Date(this.incomeTaxParameterCreateForm.value.endDate), 'yyyy-MM-dd');
    let startDate = this.datePipe.transform(new Date(this.incomeTaxParameter.startDate), 'yyyy-MM-dd');
    let endDate = this.datePipe.transform(new Date(this.incomeTaxParameter.endDate), 'yyyy-MM-dd');
    let parameterName = this.incomeTaxParameter.parameterName;
    if(editedParameterName === parameterName && startDate === editedStartDate && endDate === editedEndDate )
      {
        return false;   
      }
      return true;


 
  }
  onFocus():void{
    this.isDateOverLap = false;
  }
}
