import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-employee-detail-base',
  templateUrl: './employee-detail-base.component.html',
  styleUrls: ['./employee-detail-base.component.css']
})


export class EmployeeDetailBaseComponent implements OnInit {

  dataLoaded: Promise<boolean>;

  userInfo: any;
  canCreate: boolean = true;
  selectedsiteAdminName: string;
  siteAdminSubMenu: any[] = [];
  selectedSubMenuIndex: number = 0;
  activeSubMenu: string = "Gifts";
  state$: Observable<object>;
  fullName: any;
  employeeId: any;

  constructor(public route: ActivatedRoute) {
  }

  ngOnInit() {
    this.state$ = this.route.paramMap
      .pipe(map(() => window.history.state));

    this.employeeId = this.route.snapshot.params.id;
    this.fullName = this.route.snapshot.params.name;
    this.siteAdminSubMenu.push({ "name": "Gifts", "dataTarget": "giftsTab" });
    this.siteAdminSubMenu.push({ "name": "Gifts", "dataTarget": "giftsTab" });
    this.siteAdminSubMenu.push({ "name": "Gifts", "dataTarget": "giftsTab" });
    this.siteAdminSubMenu.push({ "name": "Gifts", "dataTarget": "giftsTab" });
    this.siteAdminSubMenu.push({ "name": "Gifts", "dataTarget": "giftsTab" });


    this.dataLoaded = Promise.resolve(true);

  }

  onSubMenuChange(name, index) {
    if (this.selectedSubMenuIndex != index) {
      this.selectedSubMenuIndex = index === this.selectedSubMenuIndex ? -1 : index;
      this.activeSubMenu = this.siteAdminSubMenu.filter(c => c.name === name)[0].name;
    }
  }


}

