import { Injector } from '@angular/core';
import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BenefitDeductionConfig, BenefitDeductionEmployee } from 'src/app/shared/models';
import { BenefitDeductionConfigService, BenefitDeductionEmployeeService, BenefitDeductionService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateBenefitDeductionComponent } from '../create-benefit-deduction/create-benefit-deduction.component';
import { CreateBenefitDeductionConfigComponent } from './create-benefit-deduction-config/create-benefit-deduction-config.component';
import { CreateEmployeeBenefitDeductionComponent } from './create-employee-benefit-deduction/create-employee-benefit-deduction.component';
@Component({
  selector: 'app-benefit-deduction-config',
  templateUrl: './benefit-deduction-config.component.html',
  styleUrls: ['./benefit-deduction-config.component.css']
})
export class BenefitDeductionConfigComponent extends BaseAuthorizedComponent implements OnInit {
  @Input() benefitDeductionCode: any;
  @Input() companyId: any;
  @Input() parentIndex: any;
  @Input() benefitDeductionLen: any;
  benefitDeductionConfigs: any;
  isFormValid: boolean;
  errorMessages: any;
  benefitDeductionConfigFilterFormGroup: FormGroup;
  loading: boolean = false;
  hideme: any[] = [];
  childList: any[] = [];
  benefitDeductionEmployeeLen: number = 100;
  benefitDeductionEmployee: BenefitDeductionEmployee;
  benefitDeductionConfigId: any;
  childIndex: any;
  constructor(
    private benefitDeductionConfigService: BenefitDeductionConfigService,
    private dialog: MatDialog,
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private injector: Injector,
    private benefitDeductionEmployeeService: BenefitDeductionEmployeeService,
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.getAllBenefitDeductionConfigsByCompanyId();
    this.setInitialTablesData();
    //this.buildForm();
  }
  getAllBenefitDeductionConfigsByCompanyId(): void {
    this.loading = true;
    this.benefitDeductionConfigService.getAllBenefitDeductionConfigByCompanyId(this.companyId).subscribe(result => {
      this.benefitDeductionConfigs = result.filter(x => x.benefitDeductionCode === this.benefitDeductionCode);
      this.loading = false;
    }, () => { this.loading = false; })
  }
  onDeleteBenefitDeductionConfig(benefitDeductionConfig: BenefitDeductionConfig): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the benefit deduction configuration " + benefitDeductionConfig.name;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBenefitDeductionConfig(benefitDeductionConfig);
      }
    });
  }
  public showChildList(index:any, structureId:any) {
    this.hideme[this.parentIndex][index] = !this.hideme[this.parentIndex][index];
  }
  createEmployeeBenefitDeduction(benefitDeductionConfig: BenefitDeductionConfig): void {
    const createEmployeeBenefitDeductionDialogConfig = new MatDialogConfig;
    createEmployeeBenefitDeductionDialogConfig.disableClose = true;
    createEmployeeBenefitDeductionDialogConfig.autoFocus = true;
    createEmployeeBenefitDeductionDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeBenefitDeductionDialogConfig.width = `${dialogWidth}%`;
    var benefitDeductionEmployee = new BenefitDeductionEmployee();
    benefitDeductionEmployee.benefitDeductionId = benefitDeductionConfig.id;
    createEmployeeBenefitDeductionDialogConfig.data = benefitDeductionEmployee;
    const createEmployeeBenefitDeductionDialog = this.dialog.open(CreateEmployeeBenefitDeductionComponent, createEmployeeBenefitDeductionDialogConfig)
    const successfullCreate = createEmployeeBenefitDeductionDialog.componentInstance.onEmployeeBenefitDeductionCreateEvent.subscribe((data) => {
    
      this.getEmployoeeBenefitDeduction(this.parentIndex, this.childIndex);
     
    });
    createEmployeeBenefitDeductionDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeBenefitDeduction(benefitDeductionEmployee: BenefitDeductionEmployee): void {
    const editEmployeeBenefitDeductionDialogConfig = new MatDialogConfig;
    editEmployeeBenefitDeductionDialogConfig.disableClose = true;
    editEmployeeBenefitDeductionDialogConfig.autoFocus = true;
    editEmployeeBenefitDeductionDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editEmployeeBenefitDeductionDialogConfig.width = `${dialogWidth}%`;
    editEmployeeBenefitDeductionDialogConfig.data = benefitDeductionEmployee;
    const editEmployeeBenefitDeductionDialog = this.dialog.open(CreateEmployeeBenefitDeductionComponent, editEmployeeBenefitDeductionDialogConfig)
    const successfullCreate = editEmployeeBenefitDeductionDialog.componentInstance.onEmployeeBenefitDeductionEditEvent.subscribe((data) => {
     
      this.getEmployoeeBenefitDeduction(this.parentIndex, this.childIndex);
     
    });
    editEmployeeBenefitDeductionDialog.afterClosed().subscribe(() => {
    });
  }
  onEditBenefitDeductionConfig(benefitDeductionConfig: BenefitDeductionConfig): void {
    const editBenefitDeductionCodeDialogConfig = new MatDialogConfig;
    editBenefitDeductionCodeDialogConfig.disableClose = true;
    editBenefitDeductionCodeDialogConfig.autoFocus = true;
    editBenefitDeductionCodeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editBenefitDeductionCodeDialogConfig.width = `${dialogWidth}%`;
    editBenefitDeductionCodeDialogConfig.data = benefitDeductionConfig;
    const editBenefitDeductionDialog = this.dialog.open(CreateBenefitDeductionConfigComponent, editBenefitDeductionCodeDialogConfig)
    const successfullCreate = editBenefitDeductionDialog.componentInstance.onBenefitDeductionConfigEditEvent.subscribe((data) => {
     
      this.getEmployoeeBenefitDeduction(this.parentIndex, this.childIndex);
      this.getAllBenefitDeductionConfigsByCompanyId();

    });
    editBenefitDeductionDialog.afterClosed().subscribe(() => {
     });
  }
  showEmployeeBenefitDeductionList(benefitDeductionConfig: BenefitDeductionConfig, parentIndex: any, index: any): void {
    this.childIndex = index;
    this.benefitDeductionConfigId = benefitDeductionConfig.id;
    this.hideme[parentIndex][index] = !this.hideme[parentIndex][index];
    this.getEmployoeeBenefitDeduction(parentIndex, index);
  }
  getEmployoeeBenefitDeduction(parentIndex: any, index: any): void {
    this.loading = true;
    this.benefitDeductionEmployeeService.getAllBenefitDeductionEmployeesByCompanyId(this.companyId).subscribe(result => {
      this.childList[parentIndex][index] = result.filter(x => x.benefitDeductionId === this.benefitDeductionConfigId);
      this.totalItems = this.childList[parentIndex][index].length;
      this.generateTotalItemsText();
      this.loading = false;
    }, () => {
    })
  }
  onDeleteBenefitDeductionEmployee(benefitDeductionEmployee: BenefitDeductionEmployee): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the Employee benefit deduction ";
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBenefitDeductionForEmployee(benefitDeductionEmployee);
      }
    });
  }
  deleteBenefitDeductionConfig(benefitDeductionConfig: BenefitDeductionConfig): void {
    this.benefitDeductionConfigService.deleteBenefitDeductionConfig(benefitDeductionConfig).subscribe(result => {
      this.alertService.success('Benefit deduction configuration deleted successfully');
      this.getAllBenefitDeductionConfigsByCompanyId();
    }, () => { })
  }
  deleteBenefitDeductionForEmployee(benefitDeductionEmployee: BenefitDeductionEmployee): void {
    this.benefitDeductionEmployeeService.deleteEmployeeBenefitDeduction(benefitDeductionEmployee).subscribe(result => {
      this.alertService.success('Employee benefit deduction successfully deleted');
      this.getEmployoeeBenefitDeduction(this.parentIndex, this.childIndex);
    }, () => { })
  }
  setInitialTablesData(): void {
    for (let i = 0; i < this.benefitDeductionLen; i++) {
      this.hideme[i] = new Array(this.benefitDeductionEmployeeLen);
      this.childList[i] = new Array(this.benefitDeductionEmployeeLen);
    }
  }
}
