import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IncomeTaxInvestment } from 'src/app/shared/models';
import { CompanyService, IncomeTaxInvestmentService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-income-tax-investment',
  templateUrl: './create-income-tax-investment.component.html',
  styleUrls: ['./create-income-tax-investment.component.css']
})
export class CreateIncomeTaxInvestmentComponent implements OnInit {
  onIncomeTaxInvestmentCreateEvent: EventEmitter<any> = new EventEmitter();
  onIncomeTaxInvestmentEditEvent: EventEmitter<any> = new EventEmitter();
  incomeTaxInvestmentCreateForm: FormGroup;
  submitted: boolean;
  incomeTaxInvestment: IncomeTaxInvestment;
  isEditMode: boolean;
  isFormValid: boolean;
  incomeTaxPayerTypes: any[] = [];
  errorMessages: any;
  loading: boolean;
  companies: any;
  incomeTaxInvestments: IncomeTaxInvestment[];
  dateOverLapErrorMessage:string= 'Dates overlapping';
  isDateOverLap: boolean;
  investMentId:any;
  constructor(
    private injector: Injector,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateIncomeTaxInvestmentComponent>,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private incomeTaxInvestmentService: IncomeTaxInvestmentService,
    private companyService: CompanyService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.incomeTaxInvestment = new IncomeTaxInvestment();
    if (isNaN(data)) {
      this.incomeTaxInvestment = new IncomeTaxInvestment(data);
    }
    this.investMentId = this.incomeTaxInvestment.id;
    if (this.incomeTaxInvestment.id) {
      this.isEditMode = true;
      this.buildForm();
      this.checkDateOverLapsInEditMode();
    }
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllIncomeTaxInvestments();
  }
  buildForm(): void {
    this.incomeTaxInvestmentCreateForm = this.formBuilder.group({
      companyId: [this.incomeTaxInvestment.companyId],
      investmentPercentage: [this.incomeTaxInvestment.investmentPercentage, [Validators.required]],
      waiverPercentage: [this.incomeTaxInvestment.waiverPercentage, [Validators.required]],
      remarks: [this.incomeTaxInvestment.remarks, [Validators.maxLength(50)]],
      startDate: [this.incomeTaxInvestment.startDate, [Validators.required]],
      endDate: [this.incomeTaxInvestment.endDate, [Validators.required]],
    });
  }
  get f() { return this.incomeTaxInvestmentCreateForm.controls; }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
      this.loading = false;
    })
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.isEditMode) {
      if (!this.checkDateOverLapsInEditMode()) {
        this.editIncomeTaxInvestment();
      }
      else {
        if (this.checkDuplicates()) {
          return;
        }
      }
    }
    if ( !this.isEditMode && this.checkDuplicates() ) {
      return;
    }
    if (this.incomeTaxInvestmentCreateForm.invalid) {
      return;
    }
    if (this.incomeTaxInvestment.id === null || this.incomeTaxInvestment.id === undefined) {
      this.createIncomeTaxInvestment();
    }
    else {
      this.editIncomeTaxInvestment();
    }
  }
  createIncomeTaxInvestment(): void {
    this.incomeTaxInvestment = new IncomeTaxInvestment(this.incomeTaxInvestmentCreateForm.value);
    this.incomeTaxInvestment.startDate = this.datePipe.transform(new Date(this.incomeTaxInvestment.startDate), 'yyyy-MM-dd');
    this.incomeTaxInvestment.endDate = this.datePipe.transform(new Date(this.incomeTaxInvestment.endDate), 'yyyy-MM-dd');
    this.loading = true;
    this.incomeTaxInvestmentService.createIncomeTaxInvestment(this.incomeTaxInvestment).subscribe(result => {
      this.onIncomeTaxInvestmentCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Income tax investment successfully created');
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
  editIncomeTaxInvestment(): void {
    let newData = new IncomeTaxInvestment(this.incomeTaxInvestmentCreateForm.value);
    if (this.incomeTaxInvestment !== null) {
      this.incomeTaxInvestment.investmentPercentage = newData.investmentPercentage;
      this.incomeTaxInvestment.remarks = newData.remarks;
      this.incomeTaxInvestment.waiverPercentage = newData.waiverPercentage;
      this.incomeTaxInvestment.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.incomeTaxInvestment.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.loading = true;
      this.incomeTaxInvestmentService.editIncomeTaxInvestment(this.incomeTaxInvestment).subscribe(result => {
        this.onIncomeTaxInvestmentEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Income tax investment successfully updated');
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
  close(): void {
    this.dialogRef.close();
  }
  getAllIncomeTaxInvestments(): void {
    this.incomeTaxInvestmentService.getAllIncomeTaxInvestment(this.incomeTaxInvestment.companyId).subscribe(result => {
      this.incomeTaxInvestments = result;
    }, () => {
    })
  }
  checkDuplicates(): boolean {
    let isDateOverLapped = this.checktDateOverLap();
    if (isDateOverLapped) this.isDateOverLap = true;

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
    let results = this.incomeTaxInvestments;
    let startDate = this.datePipe.transform(new Date(this.incomeTaxInvestmentCreateForm.value.startDate), 'yyyy-MM-dd');
    let endDate = this.datePipe.transform(new Date(this.incomeTaxInvestmentCreateForm.value.endDate), 'yyyy-MM-dd');
    let dateOverLapped = false;
    results.find(item => {
      let edDate = this.datePipe.transform(new Date(item.endDate), 'yyyy-MM-dd');
      if (new Date(edDate).getTime() >= new Date(startDate).getTime()) {
        if (this.checktEndDateOverLap(item.startDate, endDate)) {
          if(this.incomeTaxInvestment.id !== item.id){
          dateOverLapped = true;
          return dateOverLapped;
          }
        }
      }
    });
    return dateOverLapped;
  }
  checkDateOverLapsInEditMode(): boolean {
    let editedStartDate = this.datePipe.transform(new Date(this.incomeTaxInvestmentCreateForm.value.startDate), 'yyyy-MM-dd');
    let editedEndDate = this.datePipe.transform(new Date(this.incomeTaxInvestmentCreateForm.value.endDate), 'yyyy-MM-dd');
    let startDate = this.datePipe.transform(new Date(this.incomeTaxInvestment.startDate), 'yyyy-MM-dd');
    let endDate = this.datePipe.transform(new Date(this.incomeTaxInvestment.endDate), 'yyyy-MM-dd');
    if (startDate === editedStartDate && endDate === editedEndDate) {
      return false;
    }
    return true;
  }
  onFocus(): void {
    this.isDateOverLap = false;
  }
}
