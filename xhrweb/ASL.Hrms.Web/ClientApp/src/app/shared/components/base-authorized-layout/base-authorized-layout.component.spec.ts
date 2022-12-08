import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseAuthorizedLayoutComponent } from './base-authorized-layout.component';

describe('BaseAuthorizedLayoutComponent', () => {
  let component: BaseAuthorizedLayoutComponent;
  let fixture: ComponentFixture<BaseAuthorizedLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BaseAuthorizedLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseAuthorizedLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
