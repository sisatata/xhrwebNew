import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { SalaryStructureComp } from 'src/app/shared/models/payroll/salary-structure-component';
import { CreateSalaryStructureCompComponent } from './create-salarystructurecomponent/create-salary-structure-component.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, SalaryStructureComponentService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-salary-structure-comp',
  templateUrl: './salary-structure-component.component.html',
  styleUrls: ['./salary-structure-component.component.css']
})
export class SalaryStructureCompComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('salarystructurecomponent-list-section') blockUI: NgBlockUI
  salarystructurecomponents: SalaryStructureComp[];
  salarystructurecomponentId: any;
  salarystructurecomponentFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;

  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private salarystructurecomponentService: SalaryStructureComponentService,
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
    this.salarystructurecomponentFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.salarystructurecomponentFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllSalaryStructureComponentByCompanyId();
    }
  }
  getSalaryStructureComponent() {
    this.submitted = true;
    if (this.salarystructurecomponentFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllSalaryStructureComponentByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  createSalaryStructureComponent() {
    const createSalaryStructureComponentDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createSalaryStructureComponentDialogConfig.disableClose = true;
    createSalaryStructureComponentDialogConfig.autoFocus = true;
    createSalaryStructureComponentDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createSalaryStructureComponentDialogConfig.width =  `${dialogWidth}%`;
    var salarystructurecomponent = new SalaryStructureComp();
    salarystructurecomponent.companyId = this.companyId;
    createSalaryStructureComponentDialogConfig.data = salarystructurecomponent;
    const createSalaryStructureComponentDialog = this.dialog.open(CreateSalaryStructureCompComponent, createSalaryStructureComponentDialogConfig);
    const successfullCreate = createSalaryStructureComponentDialog.componentInstance.onSalaryStructureComponentCreateEvent.subscribe((data) => {
      this.getAllSalaryStructureComponentByCompanyId();
    });
    createSalaryStructureComponentDialog.afterClosed().subscribe(() => {
    });
  }
  editSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp) {

    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = salarystructurecomponent
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreateSalaryStructureCompComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onSalaryStructureComponentEditEvent.subscribe((data) => {
      this.getAllSalaryStructureComponentByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the Salary Structure Component ' + salarystructurecomponent.salaryComponentName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteSalaryStructureComponent(salarystructurecomponent);
      }
    });
  }
  deleteSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp) {
    this.salarystructurecomponentService.deleteSalaryStructureComponent(salarystructurecomponent).subscribe((data) => {
      this.getAllSalaryStructureComponentByCompanyId();
    },
      (error) => {
        console.log(error);
      });
  }

  getAllSalaryStructureComponentById(brnchId) {
    this.salarystructurecomponentService.getSalaryStructureComponentById(brnchId).subscribe(result => {
      this.salarystructurecomponents = result;
    })
  }

  getAllSalaryStructureComponentByCompanyId() {
    this.blockUI.start();
    this.salarystructurecomponentService.getAllSalaryStructureComponentByCompanyId(this.companyId).subscribe(result => {
      this.salarystructurecomponents = result;

      this.totalItems = this.salarystructurecomponents.length;
      this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })

  }
 
}

