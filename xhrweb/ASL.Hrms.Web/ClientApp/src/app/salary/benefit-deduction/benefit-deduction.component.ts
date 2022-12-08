import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Branch } from 'src/app/shared/models/company/branch';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService, BenefitDeductionService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BenefitDeductionCode, BenefitDeductionConfig } from 'src/app/shared/models';
import { CreateBenefitDeductionComponent } from './create-benefit-deduction/create-benefit-deduction.component';
import { AlertService } from 'src/app/shared/services/alert.service';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { CreateBenefitDeductionConfigComponent } from './benefit-deduction-config/create-benefit-deduction-config/create-benefit-deduction-config.component';
@Component({
  selector: 'app-benefit-deduction',
  templateUrl: './benefit-deduction.component.html',
  styleUrls: ['./benefit-deduction.component.css']
})
export class BenefitDeductionComponent extends BaseAuthorizedComponent implements OnInit {
  submitted: boolean;
  companies: any;
  companyId: any = localStorage.getItem('companyId');
  benefitDeductionFilterFormGroup: FormGroup
  benifitDeductionCode: any;
  benifitDeductionCodeName: any;
  benefitDeductions: BenefitDeductionCode[];
  loading: boolean;
  hideme = [];
  benefitDeductionCodeLength: number;
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private benefitDeductionService: BenefitDeductionService,
    private alertService: AlertService,
    private injector: Injector) {
    super(injector);
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllBenefitDeductionsByCompanyId();
  }
  get f() { return this.benefitDeductionFilterFormGroup.controls; }
  buildForm() {
    this.benefitDeductionFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      benifitDeductionCode: [this.benifitDeductionCode, [Validators.required, Validators.maxLength(50)]],
      benifitDeductionCodeName: [this.benifitDeductionCodeName, [Validators.required, Validators.maxLength(50)]]
    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  
  createBenefitDeductionCode() {
    const createBenefitDeductionCodeDialogConfig = new MatDialogConfig;
    createBenefitDeductionCodeDialogConfig.disableClose = true;
    createBenefitDeductionCodeDialogConfig.autoFocus = true;
    createBenefitDeductionCodeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createBenefitDeductionCodeDialogConfig.width = `${dialogWidth}%`;
    var benefitDeductionCode = new BenefitDeductionCode();
    benefitDeductionCode.companyId = this.companyId;
    createBenefitDeductionCodeDialogConfig.data = benefitDeductionCode;
    const createBenefitDeductionDialog = this.dialog.open(CreateBenefitDeductionComponent, createBenefitDeductionCodeDialogConfig)
    const successfullCreate = createBenefitDeductionDialog.componentInstance.onBenefitDeductionCodeCreateEvent.subscribe((data) => {
      this.getAllBenefitDeductionsByCompanyId();
     
    });
     createBenefitDeductionDialog.afterClosed().subscribe(() => {
     });
  }
  editBenefitDeductionCode(benefitDeductionCode: BenefitDeductionCode) {
    const editBenefitDeductionCodeDialogConfig = new MatDialogConfig;
    editBenefitDeductionCodeDialogConfig.disableClose = true;
    editBenefitDeductionCodeDialogConfig.autoFocus = true;
    editBenefitDeductionCodeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editBenefitDeductionCodeDialogConfig.width = `${dialogWidth}%`;
    editBenefitDeductionCodeDialogConfig.data = benefitDeductionCode;
    const createBenefitDeductionDialog = this.dialog.open(CreateBenefitDeductionComponent, editBenefitDeductionCodeDialogConfig)
    const successfullCreate = createBenefitDeductionDialog.componentInstance.onBenefitDeductionCodeCreateEvent.subscribe((data) => {
      this.getAllBenefitDeductionsByCompanyId();
      this.hideAll();
    });
    createBenefitDeductionDialog.afterClosed().subscribe(() => {
   });
  }
  public showChildList(index, structureId) {
    this.hideme[index] = !this.hideme[index];
  }
  getAllBenefitDeductionsByCompanyId(): void {
    this.loading = true;
    this.benefitDeductionService.getAllBenifitDeductionByCompanyId(this.companyId).subscribe(result => {
      this.benefitDeductions = result;
      this.totalItems = result.length;
      this.generateTotalItemsText();
      this.loading = false;
      this.benefitDeductionCodeLength = result.length;
    }, () => { this.loading = false; })
  }
  onDeleteBenefitDeductionCode(benefitDeductionCode: BenefitDeductionCode): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the benefit deduction " + benefitDeductionCode.benifitDeductionCodeName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBenefitDeductionCode(benefitDeductionCode);
      }
    });
  }
  deleteBenefitDeductionCode(benefitDeductionCode: BenefitDeductionCode): void {
    this.benefitDeductionService.deleteBenefitDeductionCode(benefitDeductionCode).subscribe(result => {
      this.alertService.success('Benefit deduction deleted successfully');
      this.getAllBenefitDeductionsByCompanyId();
      this.hideAll();
    }, () => { })
  }
  createBenefitDeductionConfig(benefitDeductionCode: BenefitDeductionCode): void {
    const createBenefitDeductionConfigDialogConfig = new MatDialogConfig();
   
    createBenefitDeductionConfigDialogConfig.disableClose = true;
    createBenefitDeductionConfigDialogConfig.autoFocus = true;
    createBenefitDeductionConfigDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createBenefitDeductionConfigDialogConfig.width = `${dialogWidth}%`;
    var benefitDeductionConfig = new BenefitDeductionConfig();
    benefitDeductionConfig.companyId = this.companyId;
    benefitDeductionConfig.benefitDeductionCode = benefitDeductionCode.benifitDeductionCode;
    createBenefitDeductionConfigDialogConfig.data = benefitDeductionConfig;
    const createBankBranchDialog = this.dialog.open(CreateBenefitDeductionConfigComponent, createBenefitDeductionConfigDialogConfig);
    const successfullCreate = createBankBranchDialog.componentInstance.onBenefitDeductionConfigCreateEvent.subscribe((data) => {
      
      this.hideAll();
    });
    createBankBranchDialog.afterClosed().subscribe(() => {
    });
  }
  hideAll(): void {
    for (let i = 0; i < this.benefitDeductions.length; i++) {
      this.hideme[i] = false;
    }
  }
}
