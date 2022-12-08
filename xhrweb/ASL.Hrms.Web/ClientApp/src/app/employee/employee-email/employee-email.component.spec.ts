import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeEmailComponent } from './employee-email.component';

describe('EmployeeEmailComponent', () => {
  let component: EmployeeEmailComponent;
  let fixture: ComponentFixture<EmployeeEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
