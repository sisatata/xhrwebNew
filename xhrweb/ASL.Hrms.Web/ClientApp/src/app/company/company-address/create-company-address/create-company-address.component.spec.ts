import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCompanyAddressComponent } from './create-company-address.component';

describe('CreateCompanyAddressComponent', () => {
  let component: CreateCompanyAddressComponent;
  let fixture: ComponentFixture<CreateCompanyAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCompanyAddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCompanyAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
