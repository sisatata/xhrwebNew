import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateEmployeeSalaryComponent } from './create-employee-salary.component';
describe('CreateEmployeeSalaryComponent', () => {
  let component: CreateEmployeeSalaryComponent;
  let fixture: ComponentFixture<CreateEmployeeSalaryComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateEmployeeSalaryComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeSalaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


