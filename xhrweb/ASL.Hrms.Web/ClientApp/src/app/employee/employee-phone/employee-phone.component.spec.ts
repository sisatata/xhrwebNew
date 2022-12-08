import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeePhoneComponent } from './employee-phone.component';

describe('EmployeePhoneComponent', () => {
  let component: EmployeePhoneComponent;
  let fixture: ComponentFixture<EmployeePhoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeePhoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeePhoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
