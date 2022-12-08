import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EmployeeSalaryComp } from 'src/app/shared/models/payroll/employee-salary-comp';
import { CreateEmployeeSalaryCompComponent } from './create-employee-salary-comp/create-employee-salary-comp.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, EmployeeSalaryCompService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'employee-salary-comp',
  templateUrl: './employee-salary-comp.component.html',
  styleUrls: ['./employee-salary-comp.component.css']
})
export class EmployeeSalaryCompComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('employeesalarycomponent-list-section') blockUI: NgBlockUI
  employeeSalaryComps: EmployeeSalaryComp[];
  employeeSalaryComponentId: any;
  employeeSalaryCompFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;

  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private employeeSalaryCompService: EmployeeSalaryCompService,
    private companyService: CompanyService,
    private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }

  buildForm() {
    this.employeeSalaryCompFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.employeeSalaryCompFilterFormGroup.controls; }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllEmployeeSalaryCompByCompanyId();
    }
  }
  getEmployeeSalaryComponent() {
    this.submitted = true;
    if (this.employeeSalaryCompFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllEmployeeSalaryCompByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  createEmployeeSalaryComp() {
    const createEmployeeSalaryComponentDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeSalaryComponentDialogConfig.disableClose = true;
    createEmployeeSalaryComponentDialogConfig.autoFocus = true;
    createEmployeeSalaryComponentDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeSalaryComponentDialogConfig.width = `${dialogWidth}%`;
    var employeesalarycomponent = new EmployeeSalaryComp();
    employeesalarycomponent.companyId = this.companyId;
    createEmployeeSalaryComponentDialogConfig.data = employeesalarycomponent;
    const createEmployeeSalaryComponentDialog = this.dialog.open(CreateEmployeeSalaryCompComponent, createEmployeeSalaryComponentDialogConfig);
    const successfullCreate = createEmployeeSalaryComponentDialog.componentInstance.onEmployeeSalaryComponentCreateEvent.subscribe((data) => {
      this.getAllEmployeeSalaryCompByCompanyId();
    });
    createEmployeeSalaryComponentDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeSalaryComp(employeeSalaryComp: EmployeeSalaryComp) {

    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeSalaryComp
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreateEmployeeSalaryCompComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onEmployeeSalaryComponentEditEvent.subscribe((data) => {
      this.getAllEmployeeSalaryCompByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteEmployeeSalaryComponent(employeeSalaryComp: EmployeeSalaryComp): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the EmployeeSalaryComponent ' + employeeSalaryComp.id;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeSalaryComponent(employeeSalaryComp);
      }
    });
  }
  deleteEmployeeSalaryComponent(employeesalarycomponent: EmployeeSalaryComp) {
    // this.employeeSalaryCompService.deleteEmployeeSalaryComponent(employeesalarycomponent).subscribe((data) => {
    //   this.getAllEmployeeSalaryCompByCompanyId();
    // },
    //   (error) => {
    //     console.log(error);
    //   });
  }

  getAllEmployeeSalaryComponentById(brnchId) {
    // this.employeeSalaryCompService.getEmployeeSalaryComponentById(brnchId).subscribe(result => {
    //   this.employeeSalaryComps = result;
    // })
  }

  getAllEmployeeSalaryCompByCompanyId() {
    this.blockUI.start();
    // this.employeeSalaryCompService.getAllEmployeeSalaryComponentByCompanyId(this.companyId).subscribe(result => {
    //   this.employeeSalaryComps = result;

    //   this.totalItems = this.employeeSalaryComps.length;
    //   this.generateTotalItemsText();

    //   this.blockUI.stop();
    // }, error => {
    //   this.blockUI.stop();
    // })

  }
}

