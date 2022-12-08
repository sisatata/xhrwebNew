import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { exit } from 'process';
import { IncomeTaxPayerType, IncomeTaxSlab } from 'src/app/shared/models';
import { CompanyService, IncomeTaxPayerTypeService, IncomeTaxSlabService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-income-tax-slab',
  templateUrl: './create-income-tax-slab.component.html',
  styleUrls: ['./create-income-tax-slab.component.css']
})
export class CreateIncomeTaxSlabComponent implements OnInit {
  onIncomeTaxSlabCreateEvent: EventEmitter<any> = new EventEmitter();
  onIncomeTaxSlabEditEvent: EventEmitter<any> = new EventEmitter();
  incomeTaxSlabCreateForm: FormGroup;
  submitted: boolean;
  incomeTaxPayerType: IncomeTaxPayerType;
  incomeTaxSlab: IncomeTaxSlab;
  isEditMode: boolean;
  isFormValid: boolean;
  incomeTaxPayerTypes: any[] = [];
  errorMessages: any;
  loading: boolean;
  companies: any;
  incomeTaxSlabs: IncomeTaxSlab[];
  dateOverLapErrorMessage:string= 'Dates overlapping for slab name';
  isDateOverLap:boolean = false;
  slabId:any;
  constructor(
    private injector: Injector,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateIncomeTaxSlabComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private incomeTaxPayerTypeService: IncomeTaxPayerTypeService,
    private incomeTaxSlabService: IncomeTaxSlabService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data) {
    this.incomeTaxSlab = new IncomeTaxSlab();
    if (isNaN(data)) {
      this.incomeTaxSlab = new IncomeTaxSlab(data);
    }
    this.slabId = this.incomeTaxSlab.id;
    if (this.incomeTaxSlab.id) {
      this.isEditMode = true;
      this.buildForm();
      this.checkDateOverLapsInEditMode();
    }
    this.getAllIncomeTaxSlabs();
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllIncomeTaxPayerType();
  }
  buildForm(): void {
    this.incomeTaxSlabCreateForm = this.formBuilder.group({
      companyId: [this.incomeTaxSlab.companyId],
      slabName: [this.incomeTaxSlab.slabName, [Validators.required, Validators.maxLength(50)]],
      taxPayerTypeCode: [this.incomeTaxSlab.taxPayerTypeCode],
      startDate: [this.incomeTaxSlab.startDate, [Validators.required]],
      endDate: [this.incomeTaxSlab.endDate, [Validators.required]],
      isRunningFlag: [this.incomeTaxSlab.isRunningFlag],
      slabAmount: [this.incomeTaxSlab.slabAmount, [Validators.required]],
      taxablePercent: [this.incomeTaxSlab.taxablePercent, [Validators.required]],
      description: [this.incomeTaxSlab.description, [Validators.maxLength(50)]],
      slabOrder: [this.incomeTaxSlab.slabOrder],
    });
  }
  get f() { return this.incomeTaxSlabCreateForm.controls; }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
      this.loading = false;
    })
  }
  getAllIncomeTaxPayerType(): void {
    //this.loading = true;
    this.incomeTaxPayerTypeService.getAllIncomeTaxPayerType(this.incomeTaxSlab.companyId).subscribe(result => {
      this.incomeTaxPayerTypes = result;
      // for setting as none
      this.incomeTaxPayerTypes.unshift({ id: 1, payerTypeName: 'None', payerTypeCode: 'None' });
    }, () => {
      this.loading = false;
    })
  }
  close(): void {
    this.dialogRef.close();
  }
  onSubmit(): void {
    this.submitted = true;
    if(this.isEditMode){
      if(!this.checkDateOverLapsInEditMode()){
        this.editIncomeTaxSlab();
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
    if (this.incomeTaxSlabCreateForm.invalid) {
      return;
    }
    if (this.incomeTaxSlab.id === null || this.incomeTaxSlab.id === undefined) {
      this.createIncomeTaxSlab();
    }
    else {
      this.editIncomeTaxSlab();
    }
  }
  onChangeIncomeTaxPayerType(data: any): void {
    this.incomeTaxSlab.taxPayerTypeCode = data.value;
  }
  createIncomeTaxSlab(): void {
    this.incomeTaxSlab = new IncomeTaxSlab(this.incomeTaxSlabCreateForm.value);
    // check if none thne empty string
    this.incomeTaxSlab.taxPayerTypeCode = this.incomeTaxSlab.taxPayerTypeCode === 'None' ? '' : this.incomeTaxSlab.taxPayerTypeCode;
    this.incomeTaxSlab.startDate = this.datePipe.transform(new Date(this.incomeTaxSlab.startDate), 'yyyy-MM-dd');
    this.incomeTaxSlab.endDate = this.datePipe.transform(new Date(this.incomeTaxSlab.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.incomeTaxSlabService.createIncomeTaxSlab(this.incomeTaxSlab).subscribe(result => {
      this.onIncomeTaxSlabCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Income tax slab successfully created');
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
  editIncomeTaxSlab(): void {
    let newData = new IncomeTaxSlab(this.incomeTaxSlabCreateForm.value);
    if (this.incomeTaxSlab !== null) {
      this.incomeTaxSlab.isRunningFlag = newData.isRunningFlag;
      this.incomeTaxSlab.slabAmount = newData.slabAmount;
      this.incomeTaxSlab.slabName = newData.slabName;
      this.incomeTaxSlab.description = newData.description;
      this.incomeTaxSlab.slabOrder = newData.slabOrder;
      this.incomeTaxSlab.taxPayerTypeCode = newData.taxPayerTypeCode;
      this.incomeTaxSlab.taxablePercent = newData.taxablePercent;
      this.incomeTaxSlab.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.incomeTaxSlab.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.incomeTaxSlab.taxPayerTypeName = newData.taxPayerTypeName;
      this.loading = true;
      this.incomeTaxSlabService.editIncomeTaxSlab(this.incomeTaxSlab).subscribe(result => {
        this.onIncomeTaxSlabEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Income tax slab successfully updated');
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
  }
  getAllIncomeTaxSlabs(): void {

    this.incomeTaxSlabService.getAllIncomeTaxSlab(this.incomeTaxSlab.companyId).subscribe(result => {
      this.incomeTaxSlabs = result;

    }, () => {

    })
  }
  checkDuplicates():boolean {
    let isDateOverLapped = this.checktDateOverLap();
    if(isDateOverLapped)  this.isDateOverLap = true;
    return isDateOverLapped; 

  }
  checktDateOverLap(): boolean {

    let results = this.incomeTaxSlabs.filter(item => item.slabName === this.incomeTaxSlabCreateForm.value.slabName);
    let startDate = this.datePipe.transform(new Date(this.incomeTaxSlabCreateForm.value.startDate), 'yyyy-MM-dd');
    let endDate = this.datePipe.transform(new Date(this.incomeTaxSlabCreateForm.value.endDate), 'yyyy-MM-dd');
    let dateOverLapped = false;
    results.find(item => {
      let edDate = this.datePipe.transform(new Date(item.endDate), 'yyyy-MM-dd');
      if (new Date(edDate).getTime() >= new Date(startDate).getTime()) {

        if (this.checktEndDateOverLap(item.startDate, endDate)) {

          if(item.id !== this.slabId){
          dateOverLapped = true;
          return dateOverLapped;
          }
          

        }



      }
    });

    return dateOverLapped;

  }
  checktEndDateOverLap(startDate: any, endDate: any): boolean {

    let edDate = this.datePipe.transform(new Date(endDate), 'yyyy-MM-dd');
    let stDate = this.datePipe.transform(new Date(startDate), 'yyyy-MM-dd');
    if (new Date(edDate).getTime() >= new Date(stDate).getTime()) {

      return true;
    }
    return false;
  }
  checkDateOverLapsInEditMode():boolean{
    let editedSlabName = this.incomeTaxSlabCreateForm.value.slabName;
    let editedStartDate =  this.datePipe.transform(new Date(this.incomeTaxSlabCreateForm.value.startDate), 'yyyy-MM-dd');
    let editedEndDate = this.datePipe.transform(new Date(this.incomeTaxSlabCreateForm.value.endDate), 'yyyy-MM-dd');
    let startDate = this.datePipe.transform(new Date(this.incomeTaxSlab.startDate), 'yyyy-MM-dd');
    let endDate = this.datePipe.transform(new Date(this.incomeTaxSlab.endDate), 'yyyy-MM-dd');
    let slabName = this.incomeTaxSlab.slabName;
    if(slabName === editedSlabName && startDate === editedStartDate && endDate === editedEndDate )
      {
        return false;
      }
      return true;


 
  }
  onFocus():void{
    this.isDateOverLap = false;
  }
}
