import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { CompanyEmailService, CompanyService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CompanyEmail } from '../../shared/models';
import { CreateCompanyEmailComponent } from './create-company-email/create-company-email.component';
@Component({
  selector: 'app-company-email',
  templateUrl: './company-email.component.html',
  styleUrls: ['./company-email.component.css']
})
export class CompanyEmailComponent implements OnInit {
  @Input() companyId: any;
  @Input() companyName: any;
  @BlockUI('company-email-section') blockUI: NgBlockUI;
 companyEmails =[];
 loading: boolean =false;
  constructor( private companyService: CompanyService,
    private alertService: AlertService,
    private companyEmailService: CompanyEmailService,
    private dialog: MatDialog) { }
    isFormValid: boolean;
    errorMessages: any ;
   
  ngOnInit(): void {
    this.getCompanyEmails();
  }

  getCompanyEmails():void{
    this.blockUI.start();
    this.loading = true;
   this.companyEmailService.getCompanyEmailByCompanyId(this.companyId).subscribe(result=>{
         this.blockUI.stop();
          this.isFormValid = true;
          this.companyEmails = result;
          this.loading = false;
          
   }, ()=>{
     this.blockUI.stop();
     this.loading = false;

   })
    
  }
  createCompanyEmail() {
    const createCompanyEmailDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createCompanyEmailDialogConfig.disableClose = true;
    createCompanyEmailDialogConfig.autoFocus = true;
    createCompanyEmailDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createCompanyEmailDialogConfig.width =  `${dialogWidth}%`;
    var companyEmail = new CompanyEmail();
    companyEmail.companyId = this.companyId;
    createCompanyEmailDialogConfig.data = companyEmail;
    const createCompanyEmailDialog = this.dialog.open(CreateCompanyEmailComponent, createCompanyEmailDialogConfig);
    const successfullCreate = createCompanyEmailDialog.componentInstance.onCompanyEmailCreateEvent.subscribe((data) => {
      this.getCompanyEmails();
    });
    createCompanyEmailDialog.afterClosed().subscribe(() => {
    });
  }

  editCompanyEmail(companyEmail: CompanyEmail) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = companyEmail;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;

    const editDialog = this.dialog.open(CreateCompanyEmailComponent, editDialogConfig);
    const successFullEdit = editDialog.componentInstance.onCompanyEmailEditEvent.subscribe((data) => {
      this.getCompanyEmails();
    });
    editDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteCompanyEmail(companyEmail: CompanyEmail): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the company Email ?";
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteCompanyEmail(companyEmail);
      }
    });
  }
  deleteCompanyEmail(companyEmail: CompanyEmail) {
    this.companyEmailService.deleteCompanyEmail(companyEmail).subscribe((data) => {
       if((data as any).status === true){
         this.isFormValid = true;
         this.alertService.success('Employee Email deleted successfully');
         this.getCompanyEmails();
       }
       else{
         this.isFormValid = false;
         this.errorMessages = (data as any).message; 
       }
     
    },
      (error) => {
        this.alertService.error(error);
        console.log(error);
      });
  }

}


