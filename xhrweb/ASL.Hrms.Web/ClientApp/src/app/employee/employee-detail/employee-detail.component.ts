import { Component, OnInit, Input, } from '@angular/core';
import { EmployeeDetailService, CommonLookUpTypeService } from '../../shared/services';
import { EmployeeDetail } from '../../shared/models';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormBuilder, FormGroup, Validators, AbstractControl, FormControl, ValidatorFn } from '@angular/forms';
import { AlertService } from '../../shared/services/alert.service';
import { resourceUsage } from 'process';
import { AuthService } from 'src/app/auth/services/auth.service';

// custom NID validator
function nIDValidator(data: string): ValidatorFn {
  return (c: AbstractControl): { [key: string]: boolean } | null => {
    if (c.value !== '' && (c.value.length !== 10 && c.value.length !== 13 && c.value.length !== 17) || isNaN(c.value)) {
      return { range: true };
    }
    return null;
  };
}
@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  panelOpenState = false;
  @BlockUI('employee-detail-section') blockUI: NgBlockUI;
  employeeDetail: EmployeeDetail = new EmployeeDetail();
  maritalStatuses: any;
  religions: any;
  nid: any = '';
  bloodGroups: any;
  isNIDValid: boolean = true;
  employeeDetailForm: FormGroup;
  disableBtn = false;
  companyId: any = localStorage.getItem('companyId'); // It will come after user login. Now set as default;
  submitted: boolean;
  isFormValid: boolean = true;
  errorMessages: any;
  loading: boolean = false;
  isAdmin: boolean;
  isHRManager:boolean;
  constructor(private formBuilder: FormBuilder, private employeeDetailService: EmployeeDetailService,
    private alertService: AlertService,
    private authService: AuthService,
    private commonLookUpTypeService: CommonLookUpTypeService) { }
  ngOnInit() {
    this.isAdmin = this.authService.isAdmin;
    this.isHRManager = this.authService.isHRManager;
    this.getEmployeeDetail();
    this.getAllMaritalStatuses();
    this.getAllReligions();
    this.getAllBloodGroups();
    this.buildForm();
  }
  get f() { return this.employeeDetailForm.controls; }
  getAllMaritalStatuses() {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "MaritalStatus").subscribe(result => {
      this.maritalStatuses = result;
    });
  }
  getAllReligions() {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "Religion").subscribe(result => {
      this.religions = result;
    });
  }
  getAllBloodGroups() {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "BloodGroup").subscribe(result => {
      this.bloodGroups = result
    });
  }
  buildForm() {
    this.employeeDetailForm = this.formBuilder.group({
      fathersName: [this.employeeDetail.fathersName, [Validators.required, Validators.maxLength(50)]],
      mothersName: [this.employeeDetail.mothersName, [Validators.required, Validators.maxLength(50)]],
      spouseName: [this.employeeDetail.spouseName, [Validators.maxLength(50)]],
      nid: [this.employeeDetail.nid, [nIDValidator(this.nid)]],
      bid: [this.employeeDetail.bid, [Validators.maxLength(20)]],
      maritalStatusId: [this.employeeDetail.maritalStatusId],
      religionId: [this.employeeDetail.religionId],
      bloodGroupId: [this.employeeDetail.bloodGroupId],
    });
  }
  getEmployeeDetail() {
    if (this.employeeId) {
      this.blockUI.start();
      this.employeeDetailService.getEmployeeDetailByEmployeeId(this.employeeId).subscribe(result => {
        if (result) {
          this.employeeDetail = result;
          this.buildForm();
        }
        this.blockUI.stop();
      }, error => {
        this.blockUI.stop();
      });
    }
  }
  saveEmployeeDetail() {
    this.submitted = true;
    // stop here if form is invalid
 
    if (this.employeeDetailForm.invalid) {
      return;
    }
    if (this.employeeDetail.id === undefined) {
      this.createEmployeeDetail();
    }
    else {
      this.editEmployeeDetail();
    }
  }
  createEmployeeDetail() {
    this.employeeDetail = new EmployeeDetail(this.employeeDetailForm.value);
    this.employeeDetail.employeeId = this.employeeId;
    this.loading = true;
    this.employeeDetailService.createEmployeeDetail(this.employeeDetail).subscribe(result => {
      if (result && (result as any).status == true) {
        this.employeeDetail.id = (result as any).id;
        this.alertService.success("Employee Detail Saved Successfully");
      }
      else {
        this.alertService.error((result as any).message);
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    });
  }
  editEmployeeDetail() {
    let newData = new EmployeeDetail(this.employeeDetailForm.value);
    this.employeeDetail.employeeId = this.employeeId;
    this.employeeDetail.fathersName = newData.fathersName;
    this.employeeDetail.mothersName = newData.mothersName;
    this.employeeDetail.spouseName = newData.spouseName;
    this.employeeDetail.nid = newData.nid;
    this.employeeDetail.bid = newData.bid;
    this.employeeDetail.maritalStatusId = newData.maritalStatusId;
    this.employeeDetail.religionId = newData.religionId;
    this.employeeDetail.bloodGroupId = newData.bloodGroupId;
    this.loading = true;
    this.employeeDetailService.editEmployeeDetail(this.employeeDetail).subscribe(result => {
      if (result && (result as any).status == true) {
        this.alertService.success("Employee Detail edited Successfully");
      } else {
        this.alertService.error((result as any).message);
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    });
  }
}
