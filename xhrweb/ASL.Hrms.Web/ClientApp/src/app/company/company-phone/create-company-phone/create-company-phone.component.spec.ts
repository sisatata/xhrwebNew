import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCompanyPhoneComponent } from './create-company-phone.component';

describe('CreateCompanyPhoneComponent', () => {
  let component: CreateCompanyPhoneComponent;
  let fixture: ComponentFixture<CreateCompanyPhoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCompanyPhoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCompanyPhoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
