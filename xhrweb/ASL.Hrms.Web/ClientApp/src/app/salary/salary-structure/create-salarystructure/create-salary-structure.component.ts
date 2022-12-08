import { DatePipe } from '@angular/common';
import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalaryStructure } from 'src/app/shared/models';
import { CompanyService, SalaryStructureService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-salary-structure',
  templateUrl: './create-salary-structure.component.html',
  styleUrls: ['./create-salary-structure.component.css']
})
export class CreateSalaryStructureComponent implements OnInit {
  onSalaryStructureCreateEvent: EventEmitter<any> = new EventEmitter();
  onSalaryStructureEditEvent: EventEmitter<any> = new EventEmitter();
  salarystructureCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  salaryStructure: SalaryStructure = new SalaryStructure();
  salarystructureId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateSalaryStructureComponent>,
    private formBuilder: FormBuilder,
    private salarystructureservice: SalaryStructureService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data) {
    this.salaryStructure = new SalaryStructure();
    if (isNaN(data)) {
      this.salaryStructure = new SalaryStructure(data);
      this.companyId = this.salaryStructure.companyId;
    } else {
    }
    if (this.salaryStructure.id) {
      this.isEditMode = true;
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
    this.salarystructureCreateForm = this.formBuilder.group({
      id: [this.salaryStructure.id],
      structureName: [this.salaryStructure.structureName, [Validators.maxLength(50)]],
      description: [this.salaryStructure.description, [Validators.maxLength(250)]],
      startDate: [this.salaryStructure.startDate],
      endDate: [this.salaryStructure.endDate],
      companyId: [this.salaryStructure.companyId],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.salarystructureCreateForm.invalid) {
      return;
    }
    if (this.salaryStructure.id === undefined) {
      this.createSalaryStructure();
    }
    else {
      this.editSalaryStructure();
    }
    this.dialogRef.close();
  }
  createSalaryStructure() {
    this.salaryStructure = new SalaryStructure(this.salarystructureCreateForm.value);
    this.salaryStructure.startDate = this.datePipe.transform(new Date(this.salaryStructure.startDate), 'yyyy-MM-dd');
    this.salaryStructure.endDate = this.datePipe.transform(new Date(this.salaryStructure.endDate), 'yyyy-MM-dd');
    this.salarystructureservice.createSalaryStructure(this.salaryStructure).subscribe((data: any) => {
      this.loading = true;
      this.onSalaryStructureCreateEvent.emit(this.salaryStructure.id);
      this.alertService.success('Salary Structure added successfully');
    }, (error: any) => {
      this.alertService.error(error);
    });
  }
  editSalaryStructure() {
    let newData = new SalaryStructure(this.salarystructureCreateForm.value);
    this.salaryStructure = Object.assign({}, newData);
    if (this.salaryStructure !== null) {
      this.salaryStructure.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.salaryStructure.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.loading = true;
      this.salarystructureservice.editSalaryStructure(this.salaryStructure).subscribe((data: any) => {
        this.onSalaryStructureEditEvent.emit(this.salaryStructure.id)
        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Salary Structure updated successfully');
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
  get f() { return this.salarystructureCreateForm.controls; }
}
