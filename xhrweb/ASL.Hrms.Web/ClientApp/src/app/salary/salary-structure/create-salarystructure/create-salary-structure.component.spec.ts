import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateSalaryStructureComponent } from './create-salary-structure.component';
describe('CreateSalaryStructureComponent', () => {
  let component: CreateSalaryStructureComponent;
  let fixture: ComponentFixture<CreateSalaryStructureComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateSalaryStructureComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateSalaryStructureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


