import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
@Component({
  selector: 'app-company-detail-base',
  templateUrl: './company-detail-base.component.html',
  styleUrls: ['./company-detail-base.component.css']
})
export class CompanyDetailBaseComponent implements OnInit {
  dataLoaded: Promise<boolean>;
  siteAdminSubMenu: any[] = [];
  selectedSubMenuIndex: number = 0;
  activeSubMenu: string = "Gifts";
  state$: Observable<object>;
  companyId: any;
  companyShortName: any;
  companySlogan: any;
  isActive: boolean;
  companyName: any;
  companyWebsite: any;
  facebookLink: any;
  companyLogo: any;
  constructor(public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.state$ = this.route.paramMap
      .pipe(map(() => window.history.state));

    this.companyId = this.route.snapshot.params.id;
    this.companyName = this.route.snapshot.params.name;
    this.companySlogan = this.route.snapshot.params.slogan;
    this.companyShortName = this.route.snapshot.params.sname;
    this.companyWebsite = this.route.snapshot.params.website;
    this.facebookLink = this.route.snapshot.params.facebook;
    this.companyLogo = this.route.snapshot.params.logo;
    this.isActive = this.route.snapshot.params.active !== true ? false : true;
    this.dataLoaded = Promise.resolve(true);

  }
  onSubMenuChange(name, index) {
    if (this.selectedSubMenuIndex != index) {
      this.selectedSubMenuIndex = index === this.selectedSubMenuIndex ? -1 : index;
      this.activeSubMenu = this.siteAdminSubMenu.filter(c => c.name === name)[0].name;
    }
  }

}
