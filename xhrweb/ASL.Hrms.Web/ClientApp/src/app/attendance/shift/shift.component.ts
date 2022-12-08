import { Component, OnInit, Injector } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Shift } from 'src/app/shared/models/attendance/shift';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ShiftService, CompanyService } from 'src/app/shared/services';
import { CreateShiftComponent } from './create-shift/create-shift.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BaseAuthorizedLayoutComponent } from 'src/app/shared/components/base-authorized-layout/base-authorized-layout.component';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';

@Component({
  selector: 'app-shift',
  templateUrl: './shift.component.html',
  styleUrls: ['./shift.component.css']
})
export class ShiftComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('shift-list-section') blockUI: NgBlockUI
  shifts: Shift[]=[];
  shiftId: any;
  shiftFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;

  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private shiftService: ShiftService,
    private companyService: CompanyService,
    private injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    this.buildForm();
  }
  buildForm() {
    this.shiftFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }
  get f() { return this.shiftFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllShiftByCompanyId();
    }
  }

  getShifts() {
    this.submitted = true;
    if (this.shiftFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllShiftByCompanyId();
  }

  getAllCompanies() {

    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }


  createShift() {
    const createShiftDialogConfig = new MatDialogConfig();
    createShiftDialogConfig.disableClose = true;
    createShiftDialogConfig.autoFocus = true;
    createShiftDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createShiftDialogConfig.width =  `${dialogWidth}%`;

    var shift = new Shift();
    shift.companyId = this.companyId;
    createShiftDialogConfig.data = shift;
    const createShiftDialog = this.dialog.open(CreateShiftComponent, createShiftDialogConfig);
    const successfullCreate = createShiftDialog.componentInstance.onShiftCreateEvent.subscribe((data) => {
      this.getAllShiftByCompanyId();
    });
    createShiftDialog.afterClosed().subscribe(() => {
    });
  }

  editShift(shift: Shift) {

    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = shift;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreateShiftComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onShiftEditEvent.subscribe((data) => {
      this.getAllShiftByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteShift(shift: Shift): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the Shift " + shift.shiftName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteShift(shift);
      }
    });
  }

  deleteShift(shift: Shift) {
    this.shiftService.deleteShift(shift).subscribe((data) => {
      this.getAllShiftByCompanyId();
    }, (error) => {
      console.log(error);
    });

  }

  getAllShiftByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.shiftService.getAllShiftByCompany(this.companyId).subscribe(result => {
      this.shifts = result;
      this.loading = false;
 
      this.totalItems = this.shifts.length;
      this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })

  }

}
