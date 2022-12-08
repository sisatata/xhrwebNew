import { Component, OnInit, Input, SimpleChanges, Injector } from '@angular/core';
import { Company } from '../shared/models';
import { CompanyService } from '../shared/services';
import { CreateCompanyComponent } from './create-company/create-company.component';
import { ConfirmationDialogComponent } from '../shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from '../shared/components/base-authorized/base-authorized.component';

import {ThemePalette} from '@angular/material/core';
import {ProgressSpinnerMode} from '@angular/material/progress-spinner';
@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})

export class CompanyComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('company-list-section') blockUI: NgBlockUI;
  color: ThemePalette = 'primary';
  mode: ProgressSpinnerMode = 'determinate';
  value = 50;
  userFullName: string;
  userId: number;
  companies: Company[];
  expandedIndex: number = -1;
  isSystemAdmin = false;
  loading: boolean =true;

  constructor(
    private dialog: MatDialog,
    private companyService: CompanyService,
    private injector: Injector
  ) { 
    super(injector);
  }

  ngOnInit() {
    this.getAllCompanies();
  }

  ngOnChanges(changes: SimpleChanges) {

  }

  createCompany() {
    const createCompanyDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createCompanyDialogConfig.disableClose = true;
    createCompanyDialogConfig.autoFocus = true;
    createCompanyDialogConfig.panelClass = "xHR-dialog";
    createCompanyDialogConfig.width="50%";

    var company = new Company();
    //company.userId = this.userId;
    createCompanyDialogConfig.data = company;

    const createCompanyDialog = this.dialog.open(CreateCompanyComponent, createCompanyDialogConfig);

    const successfullCreate = createCompanyDialog.componentInstance.onCompanyCreateEvent.subscribe((data) => {
      this.getAllCompanies();
    });

    createCompanyDialog.afterClosed().subscribe(() => {
    });
  }

  editCompany(company: Company) {
    const editDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = company
    editDialogConfig.panelClass = 'xHR-dialog';
    editDialogConfig.width="50%";

    const outletEditDialog = this.dialog.open(CreateCompanyComponent, editDialogConfig);

    const successFullEdit = outletEditDialog.componentInstance.onCompanyEditEvent.subscribe((data) => {
      this.getAllCompanies();
    });

    outletEditDialog.afterClosed().subscribe(() => {
    });
  }


  onDeleteCompany(company: Company): void {
    const confirmationDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the company " + company.companyName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteCompany(company);
      }
    });
  }


  deleteCompany(company: Company) {
    this.companyService.deleteCompany(company).subscribe((data) => {
      //this.alertService.success('Outlet deleted successfully');
      this.getAllCompanies();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }


  getAllCompanies() {
    this.blockUI.start();
    this.loading = true;
    this.companyService.getAllCompanies().subscribe(result => {
      this.companies = result;
      this.totalItems = this.companies.length;
      this.loading =false;
     
      this.generateTotalItemsText();
      
      this.blockUI.stop();
    }, error => {
        this.blockUI.stop();
        this.loading =false;

    });
  }

}


