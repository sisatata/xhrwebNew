import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalaryStructure, SalaryStructureComp } from 'src/app/shared/models';
import { CompanyService, SalaryStructureService, SalaryStructureComponentService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
@Component({
  selector: 'app-create-salarystructurecomponent',
  templateUrl: './create-salary-structure-component.component.html',
  styleUrls: ['./create-salary-structure-component.component.css']
})
export class CreateSalaryStructureCompComponent implements OnInit {
  @BlockUI('salarystructure-list-section') blockUI: NgBlockUI
  onSalaryStructureComponentCreateEvent: EventEmitter<any> = new EventEmitter();
  onSalaryStructureComponentEditEvent: EventEmitter<any> = new EventEmitter();
  salarystructurecomponentCreateForm: FormGroup
  submitted = false;
  companyId: any;
  valueType: any;
  hidePercentOn: boolean;
  companies: any;
  salarystructurecomp: SalaryStructureComp = new SalaryStructureComp();
  salarystructurecomponentId: number;
  isEditMode = false;
  salaryStructures: SalaryStructure[];
  salaryStructureId: any;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateSalaryStructureCompComponent>,
    private formBuilder: FormBuilder,
    private salarystructureService: SalaryStructureService,
    private salarystructurecomponentservice: SalaryStructureComponentService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.salarystructurecomp = new SalaryStructureComp();
    if (isNaN(data)) {
      this.salarystructurecomp = new SalaryStructureComp(data);
      this.companyId = this.salarystructurecomp.companyId;
      this.hidePercentOn = true;
    } else {
    }
    if (this.salarystructurecomp.id) {
      this.isEditMode = true;
    }
    this.buildForm();
    this.getAllCompanies();
  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllSalaryStructureByCompanyId();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }
  onChangeValue() {
    this.valueType = this.f.valueType.value;
    if (this.valueType === '2')
      this.hidePercentOn = false;
    else
      this.hidePercentOn = true;
  }
  buildForm() {
    this.salarystructurecomponentCreateForm = this.formBuilder.group({
      id: [this.salarystructurecomp.id],
      salaryStrutureId: [this.salarystructurecomp.salaryStrutureId],
      salaryComponentName: [this.salarystructurecomp.salaryComponentName, [Validators.maxLength(50)]],
      value: [this.salarystructurecomp.value, [Validators.required]],
      valueType: [this.salarystructurecomp.valueType, [Validators.maxLength(5)]],
      percentOn: [this.salarystructurecomp.percentOn, [Validators.maxLength(50)]],
      companyId: [this.salarystructurecomp.companyId],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.salarystructurecomponentCreateForm.invalid) {
      return;
    }
    if (this.salarystructurecomp.id === undefined) {
      this.createSalaryStructureComponent();
    }
    else {
      this.editSalaryStructureComponent();
    }
    this.dialogRef.close();
  }
  createSalaryStructureComponent() {
    this.salarystructurecomp = new SalaryStructureComp(this.salarystructurecomponentCreateForm.value);
    this.salarystructurecomp.salaryComponentName = this.salarystructurecomp.salaryComponentName.replace(/ /g, '');
   // console.log(this.salarystructurecomp);
    this.salarystructurecomponentservice.createSalaryStructureComponent(this.salarystructurecomp).subscribe((data: any) => {
      this.onSalaryStructureComponentCreateEvent.emit(this.salarystructurecomp.id);
      this.alertService.success('Salary Structure Component added successfully');
    }, (error: any) => {
      this.alertService.error(error);
    });
  }
  valueTypes = [
    { id: '1', value: 'Fixed' },
    { id: '2', value: 'Percent' },
  ];
  percentOnTypes = [
    { id: '1', value: 'Gross' },
    { id: '2', value: 'Basic' },
  ];
  editSalaryStructureComponent() {
    let newData = new SalaryStructureComp(this.salarystructurecomponentCreateForm.value);
    newData.salaryComponentName = newData.salaryComponentName.replace(/ /g, '');
    this.salarystructurecomp = Object.assign({}, newData);
    if (this.salarystructurecomp !== null) {
      this.salarystructurecomponentservice.editSalaryStructureComponent(this.salarystructurecomp).subscribe((data: any) => {
        this.onSalaryStructureComponentEditEvent.emit(this.salarystructurecomp.id)
        this.alertService.success('Salary Structure Component updated successfully');
      }, (error: any) => {
        this.alertService.error(error);
      });
    }
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
  get f() { return this.salarystructurecomponentCreateForm.controls; }
  getAllSalaryStructureByCompanyId() {
    this.blockUI.start();
    this.salarystructureService.getAllSalaryStructureByCompanyId(this.companyId).subscribe(result => {
      this.salaryStructures = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })
  }
}
