import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, CommonLookUpTypeService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { CommonLookUpType } from '../../shared/models';
import { CreateCommonLookUpTypeComponent } from './create-common-look-up-type/create-common-look-up-type.component';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
@Component({
  selector: 'app-common-look-up-type',
  templateUrl: './common-look-up-type.component.html',
  styleUrls: ['./common-look-up-type.component.css']
})
export class CommonLookUpTypeComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('common-lookup-type-section') blockUI: NgBlockUI
  commonLookUpTypes: CommonLookUpType[] = [];
  commonLookUpTypeId: any;
  commonLookUpTypeFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;
  Index: any;
  childList: any = [];
  hideme = [];
  isSystemAdmin: boolean = false;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private alertService: AlertService,
    private companyService: CompanyService,
    private injector: Injector) {
    super(injector);
    this.isSystemAdmin = this.authService.isSystemAdmin;
  }
  ngOnInit() {
    this.isSystemAdmin = this.authService.isSystemAdmin;
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }
  buildForm() {
    this.commonLookUpTypeFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]
    });
  }
  get f() { return this.commonLookUpTypeFilterFormGroup.controls; }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    this.getAllCommonLookUpTypeByCompanyId();
  }
  getCommonLookUpTypes() {
    this.submitted = true;
    if (this.commonLookUpTypeFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllCommonLookUpTypeByCompanyId();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  public showChildList(index, code) {
    this.getAllCommonLookUpTypeByParentId(index, code);
    this.hideme[index] = !this.hideme[index];
    this.Index = index;
  }
  createCommonLookUpType() {
    const createCommonLookUpTypeDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createCommonLookUpTypeDialogConfig.disableClose = true;
    createCommonLookUpTypeDialogConfig.autoFocus = true;
    createCommonLookUpTypeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createCommonLookUpTypeDialogConfig.width = `${dialogWidth}%`;
    var commonLookUpType = new CommonLookUpType();
    commonLookUpType.companyId = this.companyId;
    createCommonLookUpTypeDialogConfig.data = commonLookUpType;
    const createcommonLookUpTypeDialog = this.dialog.open(CreateCommonLookUpTypeComponent, createCommonLookUpTypeDialogConfig);
    const successfullCreate = createcommonLookUpTypeDialog.componentInstance.onCommonLookUpTypeCreateEvent.subscribe((data) => {
      this.getAllCommonLookUpTypeByCompanyId();
    });
    createcommonLookUpTypeDialog.afterClosed().subscribe(() => {
    });
  }
  createCommonLookUpTypeFromList(parentId: any) {
    const createCommonLookUpTypeDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createCommonLookUpTypeDialogConfig.disableClose = true;
    createCommonLookUpTypeDialogConfig.autoFocus = true;
    createCommonLookUpTypeDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createCommonLookUpTypeDialogConfig.width =  `${dialogWidth}%`;
    var commonLookUpType = new CommonLookUpType();
    commonLookUpType.companyId = this.companyId;
    commonLookUpType.parentId = parentId;
    createCommonLookUpTypeDialogConfig.data = commonLookUpType;
    const createcommonLookUpTypeDialog = this.dialog.open(CreateCommonLookUpTypeComponent, createCommonLookUpTypeDialogConfig);
    const successfullCreate = createcommonLookUpTypeDialog.componentInstance.onCommonLookUpTypeCreateEvent.subscribe((data) => {
      this.getAllCommonLookUpTypeByCompanyId();
      this.hideAllChilds();
    });
    createcommonLookUpTypeDialog.afterClosed().subscribe(() => {
    });
  }
  editCommonLookUpType(commonLookUpType: CommonLookUpType) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    commonLookUpType.companyId = this.companyId;
    editDialogConfig.data = commonLookUpType
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateCommonLookUpTypeComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onCommonLookUpTypeEditEvent.subscribe((data) => {
      this.getAllCommonLookUpTypeByCompanyId();
      this.hideAllChilds();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteCommonLookUpType(commonLookUpType: CommonLookUpType): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the Common LookUp Type?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteCommonLookUpType(commonLookUpType);
      }
    });
  }
  deleteCommonLookUpType(commonLookUpType: CommonLookUpType) {
    this.commonLookUpTypeService.deleteCommonLookUpType(commonLookUpType).subscribe((data) => {
      this.alertService.success('Common LookUp Type deleted successfully');
      this.getAllCommonLookUpTypeByCompanyId();
      this.hideAllChilds();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }
  getAllCommonLookUpTypeByCompanyId() {
    if (this.companyId) {
      this.blockUI.start();
      this.loading = true;
      this.commonLookUpTypeService.getAllCommonLookUpTypeByCompanyId(this.companyId).subscribe(result => {
        this.commonLookUpTypes = result;
        this.loading = false;
        this.blockUI.stop();
      }, error => {
        this.blockUI.stop();
        this.loading = false;

      })
    }
  }
  getAllCommonLookUpTypeByParentId(index: any, code: string) {
    if (this.companyId) {
      this.blockUI.start();
     // this.loading = true;
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, code).subscribe(result => {
        this.childList[index] = result;
        this.blockUI.stop();
        this.loading = false;
      }, error => {
        this.blockUI.stop();
        this.loading = false;

      })
    }
  }
  hideAllChilds(): void {
    for (let i = 0; i < this.commonLookUpTypes.length; i++) {
      this.hideme[i] = false;
    }
  }
}
