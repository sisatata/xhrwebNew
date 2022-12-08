import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateEmployeeBankAccountComponent } from './create-employee-bank-account.component';
describe('CreateEmployeeBankAccountComponent', () => {
  let component: CreateEmployeeBankAccountComponent;
  let fixture: ComponentFixture<CreateEmployeeBankAccountComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateEmployeeBankAccountComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEmployeeBankAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


