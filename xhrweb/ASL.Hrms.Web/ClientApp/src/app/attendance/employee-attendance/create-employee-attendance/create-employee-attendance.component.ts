import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Attendance } from 'src/app/shared/models/attendance/attendance';
import { AttendanceService } from 'src/app/shared/services/attendance/attendance.service';
import { CompanyService, CommonLookUpTypeService, EmployeeService, CommonSettingsService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { DatePipe } from '@angular/common';
import * as L from "leaflet";
import {latLng, MapOptions, tileLayer,Map, Marker, icon, DivIcon, divIcon} from 'leaflet';
import { AuthService } from 'src/app/auth/services/auth.service';
import { TransitionCheckState } from '@angular/material/checkbox';
import { isNgContainer } from '@angular/compiler';
@Component({
  selector: 'app-create-employee-attendance',
  templateUrl: './create-employee-attendance.component.html',
  styleUrls: ['./create-employee-attendance.component.css']
})
export class CreateEmployeeAttendanceComponent implements OnInit {
  onAttendanceCreateEvent: EventEmitter<any> = new EventEmitter();
  onAttendanceEditEvent: EventEmitter<any> = new EventEmitter();
  isFormValid: boolean = true;
  attendanceCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  errorMessage: any;
  attendance: Attendance;
  isEditMode = false;
  companyId: any = localStorage.getItem('companyId');
  latitude:number;
  map: Map;
  longitude:number;
  isAdmin: boolean;
  employees: any;
  mapOptions: MapOptions;
  loading: boolean = false;
  loadMap:boolean = false;
  locationNotFound:boolean = false;
  isLocationMandatory:boolean = false;
  redMarker:string = 'assets/images/red';
  nonSecuredUrl:boolean = true;
  constructor(
    private dialogRef: MatDialogRef<CreateEmployeeAttendanceComponent>,
    private formBuilder: FormBuilder,
    private attendanceservice: AttendanceService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private employeeService: EmployeeService,
    private companyService: CompanyService,
    private alertService: AlertService,
    private authService: AuthService,
    private commonSettingsService: CommonSettingsService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data) {
    this.attendance = new Attendance();
    if (isNaN(data)) {
      this.attendance = new Attendance(data);
     
    }
    if (this.attendance.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.isAdmin = this.authService.isAdmin;
   
    
    this.buildForm();
  }
  async ngOnInit() {
    this.loading = true;
   await this.commonSettingsService.getEmployeeCommonSettings(this.companyId,this.employeeId).toPromise().then(res=>{
                  
                  let data = res.employeeWiseCustomConfigurationList;
                  let locationMandatoryData = data.filter(x=>x.code ==='LocattionMandatoryClockin');
                  const url = window.location.href;
                  if(url.includes('https'))
                      this.nonSecuredUrl = false;
                  //console.log(url);
                  if(locationMandatoryData[0].value === 'true' && this.nonSecuredUrl === false){
                    this.isLocationMandatory = true;
                   
                  }
                  this.loading = false;
   },()=>{})
   if(this.isLocationMandatory && this.nonSecuredUrl === false){
    this.loading = true;

  await  this.getCurrentLocation().then(res=>{
        this.latitude = res.lat;
        this.longitude = res.lng;
        this.loadMap =true;
        this.loading = false;
        
    },(err)=>{

    })
   }
   
   
   if(!this.latitude && this.isLocationMandatory){
          this.locationNotFound = true;
   }
   this.initializeMapOptions();
    this.getAllCompanies();
    this.getAllEmployees();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  buildForm() {
    this.attendanceCreateForm = this.formBuilder.group({
      overNightMark: [this.attendance.overNightMark],
      clockTimeType: [this.attendance.clockTimeType, [Validators.required]],
      remarks: [this.attendance.remarks, [Validators.maxLength(150)]],
      employeeId: [this.employeeId, [Validators.required]],
    });
  }
  onChangeEmployee() {
    this.employeeId = this.f.employeeId.value;
  }
  onSubmit() {
    this.submitted = true;
  
    if (this.attendanceCreateForm.invalid) {
      return;
    }
    if (this.attendance.id === undefined) {
      this.createAttendance();
    }
    else {
      this.editAttendance();
    }
    this.dialogRef.close();
  }
  clockTypes = [
    { id: 1, value: 'In Time' },
    { id: 2, value: 'Out Time' },
  ];
  createAttendance() {
    this.submitted = true;
    if (this.attendanceCreateForm.invalid) {
      return;
    }
    this.attendance = new Attendance(this.attendanceCreateForm.value)
    this.attendance.attendanceDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
    this.attendance.employeeId = this.employeeId; this.attendance.punchtype = 3;
   if(this.isLocationMandatory){
     this.attendance.latitude = this.latitude;
     this.attendance.longitude = this.longitude;
   }
    this.loading = true;
    this.attendanceservice.createAttendance(this.attendance).subscribe((data: any) => {
      this.onAttendanceCreateEvent.emit(this.attendance.employeeId);
      if (data.status === true) {
        this.alertService.success("Attendance added successfully");
        this.isFormValid = true;
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessage = data.message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }
  editAttendance() {
    let newData = new Attendance(this.attendanceCreateForm.value);
    if (this.attendance !== null) {
      this.attendance.employeeId = localStorage.getItem('employeeId');
      this.attendance.remarks = newData.remarks;
      if(this.isLocationMandatory){
        this.attendance.latitude = this.latitude;
        this.longitude = this.longitude;
      }
      this.loading = true;
      this.attendanceservice.editAttendance(this.attendance).subscribe((data: any) => {
        if (data.status === true) {
          this.alertService.success("Attendance edited successfully");
          this.isFormValid = true;
          this.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessage = data.message;
        }
        this.loading = false;
        this.onAttendanceEditEvent.emit(this.attendance.id)
      }, (error: any) => {
        this.showErrorMessage(error);
        this.loading = false;
      });
    }
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
  
  async getCurrentLocation(): Promise<any>
  {
    return new Promise((resolve, reject) => {

      navigator.geolocation.getCurrentPosition(resp => {

          resolve({lng: resp.coords.longitude, lat: resp.coords.latitude});
        },
        err => {
          console.log(err);
          reject(err);
         
        });
    });

  }
  onMapReady(map: Map) {
    this.map = map;
    this.addSampleMarker();
    
  }

  private initializeMapOptions() {
    let lat = this.latitude === undefined ? 51.51: this.latitude;
    let lng = this.longitude === undefined ? 0 : this.longitude;
    this.mapOptions = {
      center: latLng(lat, lng),
      zoom: 14,
      layers: [
        tileLayer(
          'http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}, {}',
          {
            maxZoom: 18,
           // attribution: '&copy; <a href="https://carto.com/">carto.com</a> contributors'
          })
      ],
    };
  }
  private addSampleMarker() {
  
        const marker = new Marker([this.latitude, this.longitude])
      .setIcon(
        icon({
          iconSize: [25, 41],
          iconAnchor: [13, 41],
          iconUrl: `${this.redMarker}.png`
        }));
       marker.addTo(this.map);
   
    
  }

  get f() { return this.attendanceCreateForm.controls; }
}
