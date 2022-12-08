import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCompanyEmailComponent } from './create-company-email.component';

describe('CreateCompanyEmailComponent', () => {
  let component: CreateCompanyEmailComponent;
  let fixture: ComponentFixture<CreateCompanyEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCompanyEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCompanyEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
