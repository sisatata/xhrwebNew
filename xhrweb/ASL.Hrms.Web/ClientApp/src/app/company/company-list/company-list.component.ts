import { Component, OnInit, Input, SimpleChanges, Injector } from '@angular/core'; import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Company, Guid, PaginatorBase } from 'src/app/shared/models';
import { CompanyService } from 'src/app/shared/services';
import { CreateCompanyComponent } from '../create-company/create-company.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AlertService } from 'src/app/shared/services/alert.service';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})


export class CompanyListComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('company-list-section') blockUI: NgBlockUI;

  aa: Guid
  submitted = false;
  userFullName: string;
  userId: number;
  companies: Company[] = [];
  expandedIndex: number = -1;
  isSystemAdmin = false;
  companyStatusFilterFormGroup: FormGroup
  selectedStatus: any;
  statuses: any[] = [];
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(
    private dialog: MatDialog,
    private companyService: CompanyService,
    private injector: Injector,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
  ) { super(injector); }

  ngOnInit() {
    this.getAllCompanies();
    this.getAllStatuses();
    this.buildForm();
  }

  ngOnChanges(changes: SimpleChanges) {

  }


  onChangeStatus(status: any) {
    this.selectedStatus = status;
    if (this.selectedStatus) {
      this.getAllCompanies();
    }
  }

  getAllStatuses() {
    this.statuses = this.sharedService.getAllCompanyStatuses();
  }


  createCompany() {
    const createCompanyDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    createCompanyDialogConfig.disableClose = true;
    createCompanyDialogConfig.autoFocus = true;
    createCompanyDialogConfig.panelClass = "xHR-dialog";
    createCompanyDialogConfig.width = "50%";

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
    editDialogConfig.width = "50%";

    const outletEditDialog = this.dialog.open(CreateCompanyComponent, editDialogConfig);

    const successFullEdit = outletEditDialog.componentInstance.onCompanyEditEvent.subscribe((data) => {
      this.getAllCompanies();
    });

    outletEditDialog.afterClosed().subscribe(() => {
    });
  }


  buildForm() {
    this.companyStatusFilterFormGroup = this.formBuilder.group({
      selectedStatus: [this.selectedStatus, Validators.required]

    });
  }

  get f() { return this.companyStatusFilterFormGroup.controls; }

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
  onDeActivateCompany(company: Company):void{
    const confirmationDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to deactivate the company " + company.companyName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deactivateCompany(company);
      }
    });
  }
  deactivateCompany(company:Company):void{
this.companyService.deactivateCompany(company).subscribe(res=>{
  this.alertService.success('Deactive company successful');
        this.getAllCompanies();
},()=>{

})
}
  onDeclineCompanyRequest(company: Company): void {
    const confirmationDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to decline the request of company " + company.companyName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.declineCompany(company);
      }
    });
  }

  declineCompany(company: Company) {
    this.companyService.declineCompanyRequest(company).subscribe((data) => {
      this.alertService.success('Company request declined successfully');
      this.getAllCompanies();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }


  onApproveCompanyRequest(company: Company): void {
    const confirmationDialogConfig = new MatDialogConfig();

    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to approve the request of company " + company.companyName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.approveCompany(company);
      }
    });
  }

  approveCompany(company: Company) {
    this.companyService.approveCompanyRequest(company).subscribe((data) => {
      this.alertService.success('Company request approved successfully');
      this.getAllCompanies();
    },
      (error) => {
        //this.alertService.error('Internal server error happen');
        console.log(error);
      });
  }


  getAllCompanies() {
    this.blockUI.start();

    if (!this.selectedStatus) {
      this.selectedStatus = null;
    }
    this.loading = true;
   
    this.companyService.getAllCompaniesByApprovalStatus(this.selectedStatus).subscribe(result => {
      if(this.selectedStatus){
      this.companies = result.filter(x=>x.approvalStatus == this.selectedStatus);
      }
      else{
        this.companies = result;
      }
      
      this.totalItems = this.companies.length;
      this.generateTotalItemsText();
      this.loading = false;

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    });
  }
  

}



