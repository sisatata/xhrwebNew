import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEmployeeManagerComponent } from './create-employee-manager.component';

describe('CreateEmployeeManagerComponent', () => {
  let component: CreateEmployeeManagerComponent;
  let fixture: ComponentFixture<CreateEmployeeManagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEmployeeManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
