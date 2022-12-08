import { Component, OnInit, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Configuration, CustomConfiguration, CustomEmployeeConfiguration } from '../../shared/models';
import { ConfigurationService, BranchService, CompanyService, CustomConfigurationService, CustomEmployeeConfigurationService } from '../../shared/services';
import { PageEvent } from '@angular/material/paginator';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { CreateDefaultConfigurationComponent } from './create-default-configuration/create-default-configuration.component';
import { CreateCustomConfigurationComponent } from './create-custom-configuration/create-custom-configuration.component';
import { CreateCustomEmployeeConfigurationComponent } from './create-custom-employee-configuration/create-custom-employee-configuration.component';
import { ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('configuration-list-section') blockUI: NgBlockUI;
  configurations: Configuration[] = [];
  configurationId: any;
  branchId: any;
  companies: any;
  hideme = [];
  hideEmployeeHistoryList: any[] = [];
  childList: any = [];
  employeeChildList: any[] = [];
  branches: any;
  configurationFilterFormGroup: FormGroup;
  submitted: boolean;
  isSystemAdmin: boolean = false;
  loading: boolean = false;
  isAdmin: boolean;
  initialCustomEmployeeHistoryLength: number = 100;
  employeesCustomConfig: CustomEmployeeConfiguration[] = [];
  employeeConfigHistory: CustomEmployeeConfiguration;
  constructor(private configurationService: ConfigurationService,
    private customConfigurationService: CustomConfigurationService,
    private customEmployeeConfigService: CustomEmployeeConfigurationService,
    private branchService: BranchService, private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialog: MatDialog, private injector: Injector) {
    super(injector);
    this.isSystemAdmin = this.authService.isSystemAdmin;
    this.isAdmin = this.authService.isAdmin;
  }
  ngOnInit() {
    // this.setInitial();
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }
  buildForm() {
    this.configurationFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      branchId: [this.branchId, Validators.required],
    });
  }
  get f() { return this.configurationFilterFormGroup.controls; }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllDefaultConfigurations();
    }
  }
  showChildList(index: any): void {
    this.hideme[index] = !this.hideme[index];
  }
  showEmployeeHistoryList(config: any, parentIndex: any, index: any): void {
    this.hideEmployeeHistoryList[parentIndex][index] = !this.hideEmployeeHistoryList[parentIndex][index];
    this.getEmployeeCustomConfigurationHistory(config, parentIndex, index);
  }
  getConfigurations() {
    this.submitted = true;
    if (this.configurationFilterFormGroup.invalid) {
      return;
    }
    this.branchId = this.f.branchId.value;
    this.getAllDefaultConfigurations();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAllBranchByCompanyId() {
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
    });
  }
  getAllDefaultConfigurations() {
    this.blockUI.start();
    this.loading = true;
    this.configurationService.getAllDefaultConfigurations(this.companyId).subscribe(result => {
      this.configurations = result;
      this.loading = false;
      this.setInitialTablesData();
      if (result) {
        this.totalItems = this.configurations.length;
        this.generateTotalItemsText();
      }
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;
    })
  }
  createConfiguration() {
    const createconfigurationDialogConfig = new MatDialogConfig;
    createconfigurationDialogConfig.disableClose = true;
    createconfigurationDialogConfig.autoFocus = true;
    createconfigurationDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;

    createconfigurationDialogConfig.width =  `${dialogWidth}%`;
    var configuration = new Configuration();
    configuration.companyId = this.companyId;
    createconfigurationDialogConfig.data = configuration;
    const createConfigurationDialog = this.dialog.open(CreateDefaultConfigurationComponent, createconfigurationDialogConfig)
    const successfullCreate = createConfigurationDialog.componentInstance.onConfigurationCreateEvent.subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    });
    createConfigurationDialog.afterClosed().subscribe(() => {
    });
  }
  editConfiguration(configuration: Configuration) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = configuration;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateDefaultConfigurationComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onConfigurationEditEvent.subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    });
    outletEditDialog.afterClosed().subscribe(() => {
      //this.hideAllSubTables();
    });
  }
  createOrEditCustomConfiguration(configuration: Configuration) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    configuration.id = configuration.customConfigurationId; // changing to custom config id
    editDialogConfig.data = configuration;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateCustomConfigurationComponent, editDialogConfig);
    const successFullCreate = outletEditDialog.componentInstance.onCustomConfigurationCreateEvent.subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    });
    const successFullEdit = outletEditDialog.componentInstance.onCustomConfigurationEditEvent.subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  createCustomEmployeeConfiguration(configuration: CustomEmployeeConfiguration, defaultValue: any): void {
    const createDialogConfig = new MatDialogConfig();
    createDialogConfig.disableClose = true;
    createDialogConfig.autoFocus = true;
    // changing to custom config id
    configuration.id = null;
    configuration.startDate = null;
    configuration.endDate = null;
    configuration.defaultValue = defaultValue
    createDialogConfig.data = configuration;
    createDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateCustomEmployeeConfigurationComponent, createDialogConfig);
    const successFullCreate = outletEditDialog.componentInstance.onCustomConfigurationEmployeeCreateEvent.subscribe((data) => {
      this.hideAllSubTables();
      this.getAllDefaultConfigurations();
    });
    // const successFullEdit = outletEditDialog.componentInstance.onCustomConfigurationEmployeeEditEvent.subscribe((data) => {
    //   this.getAllDefaultConfigurations();
    //   this.hideAllSubTables();
    // });
    outletEditDialog.afterClosed().subscribe(() => {
      this.hideAllSubTables();
      this.getAllDefaultConfigurations();
    });
  }
  editCustomEmployeeConfiguration(configuration: CustomEmployeeConfiguration, defaultValue: any): void {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    //configuration.id = configuration.id; // changing to custom config id
    configuration.defaultValue = defaultValue;
    editDialogConfig.data = configuration;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateCustomEmployeeConfigurationComponent, editDialogConfig);
    const successFullCreate = outletEditDialog.componentInstance.onCustomConfigurationEmployeeCreateEvent.subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    });
    const successFullEdit = outletEditDialog.componentInstance.onCustomConfigurationEmployeeEditEvent.subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    });
    outletEditDialog.afterClosed().subscribe(() => {
      this.hideAllSubTables();
      this.getAllDefaultConfigurations();
    });
  }
  createOrEditCustomEmployeeConfiguration(configuration: any, defaultValue: any): void {
    if (configuration.id)
      this.editCustomEmployeeConfiguration(configuration, defaultValue);
    else
      this.createCustomEmployeeConfiguration(configuration, defaultValue);
  }
  onDeleteConfiguration(configuration: Configuration): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the configuration " + configuration.code;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteConfiguration(configuration);
      }
    });
  }
  deleteConfiguration(configuration: Configuration) {
    this.configurationService.deleteConfiguration(configuration).subscribe((data) => {
      this.getAllDefaultConfigurations();
      this.hideAllSubTables();
    },
      (error) => {
        console.log(error);
      });
  }
  showConfigForEmployees(item: any, index: any): void {
    this.showChildList(index);
    this.getAllEmployeesByCompanyIdAndCode(item, index);
  }
  getAllEmployeesByCompanyIdAndCode(code: any, index: any): void {
    this.loading = true;
    this.customConfigurationService.getEmployeesByCustomConfigurationByCopanyIdAndCode(this.companyId, code).subscribe(result => {
      this.employeesCustomConfig = result;
      //const ELEMENT_DATA: result;
      this.totalItemsForChild = result.length;
      this.loading = false;
      this.childList[index] = result;
    }, () => {
      this.loading = false;
    })
  }
  getEmployeeCustomConfigurationHistory(config: any, parentIndex: any, index: any): void {
    // this.hidemeChild[index] = !this.hidemeChild[index];
    this.loading = true;
    this.customConfigurationService.getEmployeeConfigHistoryByCode(config.employeeId, config.code).subscribe(result => {
      this.employeeConfigHistory = result;
      this.employeeChildList[parentIndex][index] = result;
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  onDeleteEmployeeCustomConfig(config: any): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the company " + config.employeeName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteCustomEmployeeConfig(config);
      }
    });
  }
  deleteCustomEmployeeConfig(config: any): void {
    this.customEmployeeConfigService.deleteEmployeeCustomConfig(config).subscribe(result => {
      this.hideAllSubTables();
      this.alertService.success("Employee custom configuration successfully deleted")
    })
  }
  hideAllSubTables(): void {
    for (let i = 0; i < this.configurations.length; i++) {
      this.hideme[i] = false;
      for (let j = 0; j < this.initialCustomEmployeeHistoryLength; j++) {
        this.hideEmployeeHistoryList[i][j] = false;
      }
    }
  }
  setInitialTablesData(): void {
    for (let i = 0; i < this.configurations.length; i++) {
      this.hideEmployeeHistoryList[i] = new Array(this.initialCustomEmployeeHistoryLength);
      this.employeeChildList[i] = new Array(this.initialCustomEmployeeHistoryLength);
    }
  }
  createEmployeeConfiguration(configuration : Configuration, defaultValue:any):void{
    const createDialogConfig = new MatDialogConfig();
    createDialogConfig.disableClose = true;
    createDialogConfig.autoFocus = true;
    // changing to custom config id
    configuration.id = null;
    configuration.startDate = null;
    configuration.endDate = null;
    configuration.companyId = this.companyId;
    configuration.defaultValue = defaultValue
    createDialogConfig.data = configuration;
    createDialogConfig.panelClass = 'xHR-dialog';
    createDialogConfig.width = "50%";
    const outletEditDialog = this.dialog.open(CreateCustomEmployeeConfigurationComponent, createDialogConfig);
    const successFullCreate = outletEditDialog.componentInstance.onCustomConfigurationEmployeeCreateEvent.subscribe((data) => {
      this.hideAllSubTables();
      this.getAllDefaultConfigurations();
    });
    // const successFullEdit = outletEditDialog.componentInstance.onCustomConfigurationEmployeeEditEvent.subscribe((data) => {
    //   this.getAllDefaultConfigurations();
    //   this.hideAllSubTables();
    // });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  
}