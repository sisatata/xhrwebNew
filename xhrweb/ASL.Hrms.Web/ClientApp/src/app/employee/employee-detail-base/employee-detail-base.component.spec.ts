import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeDetailBaseComponent } from './employee-detail-base.component';

describe('EmployeeDetailBaseComponent', () => {
  let component: EmployeeDetailBaseComponent;
  let fixture: ComponentFixture<EmployeeDetailBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeDetailBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeDetailBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
