import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCustomEmployeeConfigurationComponent } from './create-custom-employee-configuration.component';

describe('CreateCustomEmployeeConfigurationComponent', () => {
  let component: CreateCustomEmployeeConfigurationComponent;
  let fixture: ComponentFixture<CreateCustomEmployeeConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCustomEmployeeConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCustomEmployeeConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
