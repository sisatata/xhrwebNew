import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserRoleDto } from 'src/app/shared/models';
import { CompanyService, EmployeeDetailService, EmployeeService, RoleManagementDataService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';
import { AnyARecord } from 'dns';
@Component({
  selector: 'app-role-management',
  templateUrl: './role-management.component.html',
  styleUrls: ['./role-management.component.css']
})
export class RoleManagementComponent implements OnInit {
  userRoles: UserRoleDto;
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};
  roleFilterFormGroup: FormGroup;
  employeeId: any;
  loading: boolean = false;
  employees: any;
  companies: any;
  submitted: boolean = false;
  companyId: any = localStorage.getItem('companyId');
  roles: UserRoleDto[];
  rolesEmployee: any;
  allColors: any[];
  defaultSelections: any[];
  users: any;
  roleNames: any[]=[];
  userNotFound:boolean= false;
  constructor(
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private companyService: CompanyService,
    private formBuilder: FormBuilder,
    private roleManagementService: RoleManagementDataService
  ) { }
  ngOnInit(
  ): void {
 
    this.getAllCompanies();
    this.getAllEmployees();
    this.getAllRoles();
    this.allColors = [{ id: 1, value: 'red' }, { id: 2, value: 'greed' }];
    this.dropdownList = [
    ];
    this.selectedItems = [
    ];
    this.dropdownSettings = {
      singleSelection: false,
      text: "Select Roles",
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      enableSearchFilter: true,
      classes: "myclass custom-class"
    };
  }
  
  getAllEmployees(): void {
    this.loading = true;
    this.employeeService.getAllEmployees().subscribe(result => {
      this.employees = result;
      this.loading = false;
    }, () => {
    })
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onSubmit(): void {
    this.submitted = true;
    this.roleNames = this.selectedItems.map(x=>x.itemName);
    
    if (this.employeeId === undefined)
      return;
      if(this.roleNames.length ===0)
      return
    this.updateRoles();
  }
  onChangeRole(data: any): void {
  }
  getAllRoles(): void {
    this.loading = true;
    this.roleManagementService.getRoles().subscribe(result => {
      this.roles = result;
      this.loading = false;
      this.addItemName(result);
    }, () => {
      this.loading = false;
    })
  }
  getRolesByEmployee(employeeId: any): void {
    this.loading = true;
    this.userNotFound=false;
    this.submitted= false;
    this.roleManagementService.getRolesByEmployee(employeeId).subscribe(result => {
      this.loading = false;
      this.users = result;
      this.selectedItems = result;
    }, () => {
      this.loading = false;
    })
  }
  onChangeEmployee(data: any): void {
    this.getRolesByEmployee(data.value);
    this.employeeId = data.value;
  }
  onItemSelect(item: any) {
  }
  onSelectAll(items: any) {
  }
  addItemName(roles: any): void {
    for (let i = 0; i < roles.length; i++) {
      roles[i].itemName = roles[i].name;
    }
    this.dropdownList = roles;
  }
  OnItemDeSelect(item: any) {
  }
  onDeSelectAll(items: any) {
  }
  updateRoles(): void {
    try{
    this.loading = true;
    let { userId } = this.users.find(item => item.id === this.employeeId);
    this.userRoles = new UserRoleDto();
    this.userRoles.userId = userId;
    let roles = this.selectedItems.map(x => x.itemName);
    this.userRoles.roles = roles;
    this.roleManagementService.assignRoles(this.userRoles).subscribe(result => {
      this.loading = false;
     
      if ((result as any) === true) {
        this.alertService.success('Roles assign success');
      }
    }, () => {
      this.loading = false;
    })
  }
  catch(ex){
    this.loading = false;
    this.userNotFound= true;
  }
  }
}
