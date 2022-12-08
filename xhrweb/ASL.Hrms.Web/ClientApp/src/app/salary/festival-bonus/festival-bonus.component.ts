import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FestivalBonus } from 'src/app/shared/models';
import { BonsuConfigurationService, CompanyService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateFestivalBonusConfigComponent } from './create-festival-bonus-config/create-festival-bonus-config.component';
@Component({
  selector: 'app-festival-bonus',
  templateUrl: './festival-bonus.component.html',
  styleUrls: ['./festival-bonus.component.css']
})
export class FestivalBonusComponent extends BaseAuthorizedComponent implements OnInit {
  festivalBonusFilterFormGroup: FormGroup;
  companyId: any = localStorage.getItem('companyId');
  festivalBonuses: FestivalBonus[]=[];
  
  companies: any;
  submitted: boolean = false;
  loading: boolean = false;
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private festivalBonusConfiguationService: BonsuConfigurationService,
    private alertService: AlertService,
    private injector: Injector
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllBonusConfiguration();
  }
  buildForm(): void {
    this.festivalBonusFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]
    });
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => { })
  }
  getAllBonusConfiguration():void{
    this.loading = true;
    this.festivalBonusConfiguationService.getAllBonusConfigurationByCompany(this.companyId).subscribe(res=>{
      this.loading = false;
      this.festivalBonuses = res;
    },()=>{
      this.loading = false;
    })
  }
  get f() { return this.festivalBonusFilterFormGroup.controls; }
  onDeleteFestivalBonus(festivalBonus: FestivalBonus): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the bonus config.';
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
 
     
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteFestivalBonus(festivalBonus);
        this.getAllBonusConfiguration();
      }
    });
  }
  deleteFestivalBonus(festivalBonus: FestivalBonus) {
    
    this.festivalBonusConfiguationService.deleteFestivalBonusConfiguration(festivalBonus).subscribe(res=>{
     
      this.alertService.success('Bonus config. successfully deleted');
     
      this.getAllBonusConfiguration();
    },()=>{
      this.loading = false;
    });
  }
  createFestivalBonusConfig() {
    const createFestivalBonusDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createFestivalBonusDialogConfig.disableClose = true;
    createFestivalBonusDialogConfig.autoFocus = true;
    createFestivalBonusDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createFestivalBonusDialogConfig.width =  `${dialogWidth}%`;
    var festivalBonusConfig = new FestivalBonus();
    festivalBonusConfig.companyId = this.companyId;
    createFestivalBonusDialogConfig.data = festivalBonusConfig;
    const createEmployeeSalaryDialog = this.dialog.open(CreateFestivalBonusConfigComponent, createFestivalBonusDialogConfig);
    const successFullEdit = createEmployeeSalaryDialog.componentInstance.onFestivalBonusConfigCreateEvent.subscribe((data) => {
      this.getAllBonusConfiguration();
    })
    createEmployeeSalaryDialog.afterClosed().subscribe(() => {
    });
  }
  editFestivalBonus(festivalBonus: FestivalBonus) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = festivalBonus
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateFestivalBonusConfigComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onFestivalBonusConfigEditEvent.subscribe((data) => {
      this.getAllBonusConfiguration();
    })
    outletEditDialog.afterClosed().subscribe(() => {
    });
 }
}


