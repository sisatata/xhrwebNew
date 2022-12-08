import { Component, OnInit, Injector } from '@angular/core';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends BaseAuthorizedComponent implements OnInit {

  constructor(private injector: Injector){
    super(injector);
  }
  
  ngOnInit(): void {
  }


}
