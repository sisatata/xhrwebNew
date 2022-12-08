import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeePhoneComponent } from './create-employee-phone.component';

describe('CreateEmployeePhoneComponent', () => {
  let component: CreateEmployeePhoneComponent;
  let fixture: ComponentFixture<CreateEmployeePhoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeePhoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeePhoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
