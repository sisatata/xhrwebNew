import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateEmployeeSalaryCompComponent } from './create-employee-salary-comp.component';
describe('CreateEmployeeSalaryComponentComponent', () => {
  let component: CreateEmployeeSalaryCompComponent;
  let fixture: ComponentFixture<CreateEmployeeSalaryCompComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateEmployeeSalaryCompComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeSalaryCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


