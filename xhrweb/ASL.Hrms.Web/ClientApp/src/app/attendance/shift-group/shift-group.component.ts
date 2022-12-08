import { Component, OnInit, Input, Injector } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ShiftGroupService, CompanyService } from '../../shared/services';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { ShiftGroup } from '../../shared/models';
import { CreateShiftGroupComponent } from './create-shift-group/create-shift-group.component';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { AlertService } from '../../shared/services/alert.service';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';

@Component({
  selector: 'app-shift-group',
  templateUrl: './shift-group.component.html',
  styleUrls: ['./shift-group.component.css']
})

export class ShiftGroupComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('shift-group-section') blockUI: NgBlockUI;

  shiftGroups: any = [];
  companies: any = [];
  shiftGroupFilterFormGroup: FormGroup;
  submitted = false;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(private shiftGroupService: ShiftGroupService, private companyService: CompanyService,
    private alertService: AlertService, private formBuilder: FormBuilder,
    private dialog: MatDialog, private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();    
    this.buildForm();
    this.getAllShiftGroups();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  buildForm() {
    this.shiftGroupFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.shiftGroupFilterFormGroup.controls; }



  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
    this.getAllShiftGroups();
    }
  }
  getShifts() {
    this.getAllShiftGroups();
  }

  getAllShiftGroups() {
    if (this.companyId) {
      this.blockUI.start();
      this.loading = true;
      this.shiftGroupService.getShiftGroupByCompanyId(this.companyId).subscribe(result => {
        this.shiftGroups = result;
        this.loading = false;

      this.totalItems = this.shiftGroups.length;
      this.generateTotalItemsText();

       this.blockUI.stop();
      }, error => {
        this.blockUI.stop();
        this.loading = false;
      });
    }
  }

  createShiftGroup() {
    const createShiftGroupDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createShiftGroupDialogConfig.disableClose = true;
    createShiftGroupDialogConfig.autoFocus = true;
    createShiftGroupDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createShiftGroupDialogConfig.width = `${dialogWidth}%`;

    var shiftGroup = new ShiftGroup();
    shiftGroup.companyId = this.companyId;
    createShiftGroupDialogConfig.data = shiftGroup;
    const createshiftGroupDialog = this.dialog.open(CreateShiftGroupComponent, createShiftGroupDialogConfig);
    const successfullCreate = createshiftGroupDialog.componentInstance.onShiftGroupCreateEvent.subscribe((data) => {
      this.getAllShiftGroups();
    });
    createshiftGroupDialog.afterClosed().subscribe(() => {
    });
  }
  editShiftGroup(shiftGroup: ShiftGroup) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = shiftGroup
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;

    const editDialog = this.dialog.open(CreateShiftGroupComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onShiftGroupEditEvent.subscribe((data) => {
      this.getAllShiftGroups();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteShiftGroup(shiftGroup: ShiftGroup): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the shiftGroup ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteShiftGroup(shiftGroup);
      }
    });
  }
  deleteShiftGroup(shiftGroup: ShiftGroup) {
    this.shiftGroupService.deleteShiftGroup(shiftGroup).subscribe((data) => {
      this.alertService.success('ShiftGroup deleted successfully');
      this.getAllShiftGroups();
    },
      (error) => {
        this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }



}


