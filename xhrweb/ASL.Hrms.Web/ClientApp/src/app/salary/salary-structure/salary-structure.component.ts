/// <reference path="../salary-structure-component/create-salarystructurecomponent/create-salary-structure-component.component.ts" />
import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { SalaryStructure } from 'src/app/shared/models/payroll/salary-structure';
import { SalaryStructureComp } from 'src/app/shared/models/payroll/salary-structure-component';
import { CreateSalaryStructureComponent } from './create-salarystructure/create-salary-structure.component';
import { CreateSalaryStructureCompComponent } from '../salary-structure-component/create-salarystructurecomponent/create-salary-structure-component.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, SalaryStructureService, SalaryStructureComponentService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-salary-structure',
  templateUrl: './salary-structure.component.html',
  styleUrls: ['./salary-structure.component.css']
})
export class SalaryStructureComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('salarystructure-list-section') blockUI: NgBlockUI
  salarystructures: SalaryStructure[];
  salarystructureId: any;
  salarystructureFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;
  Index: any;
  childList: any = [];
  hideme = [];
  loading: boolean = false;
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private salarystructureService: SalaryStructureService,
    private salaryStructureComponentService: SalaryStructureComponentService,
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
    this.salarystructureFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.salarystructureFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllSalaryStructureByCompanyId();
    }
  }
  getSalaryStructure() {
    this.submitted = true;
    if (this.salarystructureFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllSalaryStructureByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  public showChildList(index, salaryStructureId) {
    this.getAllComponentByStructureId(index, salaryStructureId);
    this.hideme[index] = !this.hideme[index];
    this.Index = index;
  }

  getAllComponentByStructureId(index: any, salaryStructureId: any) {
    if (this.companyId) {
      //this.blockUI.start();
      this.salaryStructureComponentService.getAllSalaryStructureComponentByStructureId(salaryStructureId).subscribe(result => {
        this.childList[index] = result;
        //this.blockUI.stop();
      }, error => {
        this.blockUI.stop();
      })
    }
  }

  createSalaryStructure() {
    const createSalaryStructureDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createSalaryStructureDialogConfig.disableClose = true;
    createSalaryStructureDialogConfig.autoFocus = true;
    createSalaryStructureDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createSalaryStructureDialogConfig.width = `${dialogWidth}%`;
    var salarystructure = new SalaryStructure();
    salarystructure.companyId = this.companyId;
    createSalaryStructureDialogConfig.data = salarystructure;
    const createSalaryStructureDialog = this.dialog.open(CreateSalaryStructureComponent, createSalaryStructureDialogConfig);
    const successfullCreate = createSalaryStructureDialog.componentInstance.onSalaryStructureCreateEvent.subscribe((data) => {
      this.getAllSalaryStructureByCompanyId();
    });
    createSalaryStructureDialog.afterClosed().subscribe(() => {
    });
  }
  editSalaryStructure(salarystructure: SalaryStructure) {

    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = salarystructure
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;

    editDialogConfig.width = `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreateSalaryStructureComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onSalaryStructureEditEvent.subscribe((data) => {
      this.getAllSalaryStructureByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteSalaryStructure(salarystructure: SalaryStructure): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the Salary Structure ' + salarystructure.structureName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteSalaryStructure(salarystructure);
      }
    });
  }
  deleteSalaryStructure(salarystructure: SalaryStructure) {
    this.salarystructureService.deleteSalaryStructure(salarystructure).subscribe((data) => {
      this.getAllSalaryStructureByCompanyId();
    },
      (error) => {
        console.log(error);
      });
  }

  getAllSalaryStructureById(brnchId) {
    this.salarystructureService.getSalaryStructureById(brnchId).subscribe(result => {
      this.salarystructures = result;
    })
  }

  getAllSalaryStructureByCompanyId() {
    this.blockUI.start();
    this.loading =true;
    this.salarystructureService.getAllSalaryStructureByCompanyId(this.companyId).subscribe(result => {
      this.salarystructures = result;
      this.totalItems = this.salarystructures.length;
      this.loading = false;
      this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })

  }

  createSalaryStructureComponent(salaryStrutureId: any, index: any) {
    const createSalaryStructureComponentDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createSalaryStructureComponentDialogConfig.disableClose = true;
    createSalaryStructureComponentDialogConfig.autoFocus = true;
    createSalaryStructureComponentDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;

    createSalaryStructureComponentDialogConfig.width = `${dialogWidth}%`;
    var salarystructurecomponent = new SalaryStructureComp();
    salarystructurecomponent.companyId = this.companyId;
    salarystructurecomponent.salaryStrutureId = salaryStrutureId;
    createSalaryStructureComponentDialogConfig.data = salarystructurecomponent;
    const createSalaryStructureComponentDialog = this.dialog.open(CreateSalaryStructureCompComponent, createSalaryStructureComponentDialogConfig);
    const successfullCreate = createSalaryStructureComponentDialog.componentInstance.onSalaryStructureComponentCreateEvent.subscribe((data) => {
      this.getAllComponentByStructureId(index, salaryStrutureId);
      this.hideAllChilds();
    });
    createSalaryStructureComponentDialog.afterClosed().subscribe(() => {
    });
  }

  editSalaryStructureComponent(salaryStructureComponent: SalaryStructureComp, index: any) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = salaryStructureComponent
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;

    editDialogConfig.width = `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreateSalaryStructureCompComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onSalaryStructureComponentEditEvent.subscribe((data) => {
      this.getAllComponentByStructureId(index, salaryStructureComponent.salaryStrutureId);
      this.hideAllChilds();

    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp, index: any): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the Salary Structure ' + salarystructurecomponent.salaryComponentName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteSalaryStructureComponent(salarystructurecomponent, index);
        this.hideAllChilds();

      }
    });
  }
  deleteSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp,index :any) {
    this.salaryStructureComponentService.deleteSalaryStructureComponent(salarystructurecomponent).subscribe((data) => {
      this.getAllComponentByStructureId(index, salarystructurecomponent.salaryStrutureId);
    },
      (error) => {
        console.log(error);
      });
  }

  hideAllChilds():void{
    for(let i=0;i< this.salarystructures.length;i++){
      this.hideme[i] = false;
    }

  }
}

