import { DatePipe } from "@angular/common";
import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import * as L from "leaflet";
import { ClockInTypes } from "../../../enums/enum";
import { EmployeeAttendanceDetails, Guid } from "src/app/shared/models";
import { AttendanceService } from "src/app/shared/services";
import {
  MapOptions,
} from "leaflet";
@Component({
  selector: "app-employee-attendance-details",
  templateUrl: "./employee-attendance-details.component.html",
  styleUrls: ["./employee-attendance-details.component.css"],
})
export class EmployeeAttendanceDetailsComponent implements OnInit {
  isFormValid: boolean = true;
  employeeAttendanceDetailsForm: FormGroup;
  submitted = false;
 
  employeeAttendanceDetails: EmployeeAttendanceDetails[] = [];
  errorMessage: string = "";
  requestDate: any;
  isDataEmpty: boolean = false;
  mapLoaded: boolean = false;
  employeeId: any;
  clockInMarker: string = "assets/images/";
  loading: boolean = false;
  mapData: any;
  options: MapOptions;
  map: L.Map;
  markersLayer: L.LayerGroup<any>;
  sMarkersLayer: L.LayerGroup<any>;
  iconUrl: string;
  constructor(
    private dialogRef: MatDialogRef<EmployeeAttendanceDetailsComponent>,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private attendanceService: AttendanceService,
    @Inject(MAT_DIALOG_DATA) data
  ) {
    this.employeeId = data.employeeId;
    this.requestDate = this.datePipe.transform(
      new Date(data.punchDate),
      "yyyy-MM-dd"
    );
  }

  async ngOnInit(): Promise<void> {
    this.loading = true;
    await this.attendanceService
      .getEmployeeAttendanceDetails(this.employeeId, this.requestDate)
      .toPromise()
      .then(
        (res) => {
          this.employeeAttendanceDetails = res;
          if (!res.length) {
            this.loading = false;
            this.isDataEmpty = true;
            return;
          }
          this.prepareData();
          this.mapLoaded = true;
          this.initializeMapOptions();
          this.loading = false;
        },
        () => { this.loading = false;}
      );
  }
  prepareData(): void {
    this.mapData = this.employeeAttendanceDetails.map((item) => {
      return {
        lat: item.latitude,
        lng: item.longitude,
        type:
          Number(item.clockTimeType) === ClockInTypes.InTime
            ? "Clock In"
            : Number(item.clockTimeType) === ClockInTypes.OutTime
            ? "Clock Out"
            : Number(item.clockTimeType) === ClockInTypes.CheckIn
            ? "Check In"
            : "Punch",
        address: item.clockTimeAddress,
        clockTimeType: item.clockTimeType,
        time : item.attendanceDate,
        punchTypeText: item.punchtypeText,
        id:item.id
      };
    });
  }
  private initializeMapOptions():void {
    const data = this.mapData.find(
      (x: { lat: number; lng: number }) => x.lat && x.lng
    );
 if(!data ){
   return;
 }
    this.options = {
      zoom: 8,
      center: L.latLng(data.lat, data.lng),
      layers: [
        L.tileLayer(
          "http://server.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer/tile/{z}/{y}/{x}', {}",
          {
            maxZoom: 100,
           
          }
        ),
      ],
    };
    this.markersLayer = new L.LayerGroup();
    this.iconUrl = `${this.clockInMarker}clock-in.png`;
  }
  onMapReady(map: L.Map) {
    setTimeout(() => {
      map.invalidateSize();
      this.map = map;
      map.addLayer(this.markersLayer);
      this.createStations();
    }, 200);
  }
  createStations(data?:number):void {
    let locations;
    if(data){
      this.sMarkersLayer.removeLayer(this.markersLayer);
      this.sMarkersLayer.clearLayers();
      this.markersLayer.clearLayers();
      this.markersLayer.removeLayer(this.markersLayer);
       locations = this.mapData.filter((item: { clockTimeType: number; })=> item.clockTimeType === data);
    }
    this.sMarkersLayer = new L.LayerGroup();
    const values = data?locations:this.mapData;
   
    for (const item of values) {
      let icon;
      let imgUrl =
        Number(item.clockTimeType) === ClockInTypes.InTime
          ? "clock-in"
          : Number(item.clockTimeType) === ClockInTypes.OutTime
          ? "clock-out"
          : Number(item.clockTimeType) === ClockInTypes.CheckIn
          ? "check-in"
          : "punch";
      icon = new L.DivIcon({
        html: `<img style="width:30px!important; height:30px;" src=${this.clockInMarker}${imgUrl}.png />`,
      });
      var popup = L.popup().setContent(`${item.address || ""}</br>
                 ${item.type || ""} 
    `);
      const marker = L.marker([item.lat, item.lng], { icon });
      marker.bindPopup(popup);
      this.sMarkersLayer.addLayer(marker);
    }
    this.markersLayer.addLayer(this.sMarkersLayer);
  }
  onResized() {
    if (this.map) {
      this.map.invalidateSize();
    }
  }
  close(): void {
    this.dialogRef.close();
  }
  getSpecificMarker(data:any):void{
    this.sMarkersLayer.removeLayer(this.markersLayer);
    this.sMarkersLayer.clearLayers();
    this.markersLayer.clearLayers();
    this.markersLayer.removeLayer(this.markersLayer);
    this.sMarkersLayer = new L.LayerGroup();
    let location = this.mapData.filter((item: { id: any; })=> item.id === data.id)[0];
    const item = location;
      let imgUrl =
        Number(item.clockTimeType) === ClockInTypes.InTime
          ? "clock-in"
          : Number(item.clockTimeType) === ClockInTypes.OutTime
          ? "clock-out"
          : Number(item.clockTimeType) === ClockInTypes.CheckIn
          ? "check-in"
          : "punch";
    let icon = new L.DivIcon({
        html: `<img style="width:30px!important; height:30px;" src=${this.clockInMarker}${imgUrl}.png />`,
      });
      var popup = L.popup().setContent(`${item.address || ''}</br>
                 ${item.type || ''}`);
      const marker = L.marker([item.lat, item.lng], { icon });
      marker.bindPopup(popup);
      this.sMarkersLayer.addLayer(marker);
    
    this.markersLayer.addLayer(this.sMarkersLayer);
  }
  
}
