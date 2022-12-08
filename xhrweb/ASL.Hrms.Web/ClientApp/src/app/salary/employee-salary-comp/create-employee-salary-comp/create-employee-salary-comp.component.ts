import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeSalaryComp } from 'src/app/shared/models';
import { CompanyService, EmployeeSalaryCompService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';

@Component({
  selector: 'app-create-employeesalarycomponent',
  templateUrl: './create-employeesalarycomponent.component.html',
  styleUrls: ['./create-employeesalarycomponent.component.css']
})
export class CreateEmployeeSalaryCompComponent implements OnInit {
  onEmployeeSalaryComponentCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeSalaryComponentEditEvent: EventEmitter<any> = new EventEmitter();

  employeeSalaryCompCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  employeeSalaryComp: EmployeeSalaryComp = new EmployeeSalaryComp();
  employeesalarycomponentId: number;
  isEditMode = false;

  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateEmployeeSalaryCompComponent>,
    private formBuilder: FormBuilder,
    private employeeSalaryCompService: EmployeeSalaryCompService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeSalaryComp = new EmployeeSalaryComp();
    if (isNaN(data)) {

      this.employeeSalaryComp = new EmployeeSalaryComp(data);
      this.companyId = this.employeeSalaryComp.companyId;

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
    this.employeeSalaryCompCreateForm = this.formBuilder.group({
      id: [this.employeeSalaryComp.id],
      employeeSalaryId: [this.employeeSalaryComp.employeeSalaryId],
      salaryStructureComponentId: [this.employeeSalaryComp.salaryStructureComponentId],
      componentAmount: [this.employeeSalaryComp.componentAmount],
      companyId: [this.employeeSalaryComp.companyId],


    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeSalaryCompCreateForm.invalid) {
      return;
    }
    if (this.employeeSalaryComp.id === undefined) {

      this.createEmployeeSalaryComp();
    }
    else {
      this.editEmployeeSalaryComp();
    }
    this.dialogRef.close();
  }

  createEmployeeSalaryComp() {
    this.employeeSalaryComp = new EmployeeSalaryComp(this.employeeSalaryCompCreateForm.value);
    this.employeeSalaryCompService.createEmployeeSalaryComp(this.employeeSalaryComp).subscribe((data: any) => {
      this.onEmployeeSalaryComponentCreateEvent.emit(this.employeeSalaryComp.id);
      this.alertService.success('Employee Salary Component added successfully');
    }, (error: any) => {
      this.alertService.error(error);
    });
  }


  editEmployeeSalaryComp() {
    let newData = new EmployeeSalaryComp(this.employeeSalaryCompCreateForm.value);
    if (this.employeeSalaryComp !== null) {
      this.employeeSalaryComp.id = newData.id;
      this.employeeSalaryComp.employeeSalaryId = newData.employeeSalaryId;
      this.employeeSalaryComp.salaryStructureComponentId = newData.salaryStructureComponentId;
      this.employeeSalaryComp.componentAmount = newData.componentAmount;
      this.employeeSalaryComp.companyId = newData.companyId;



      this.employeeSalaryCompService.editEmployeeSalaryComp(this.employeeSalaryComp).subscribe((data: any) => {
        this.onEmployeeSalaryComponentEditEvent.emit(this.employeeSalaryComp.id)
        this.alertService.success('Employee Salary Component updated successfully');
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
  get f() { return this.employeeSalaryCompCreateForm.controls; }
}
