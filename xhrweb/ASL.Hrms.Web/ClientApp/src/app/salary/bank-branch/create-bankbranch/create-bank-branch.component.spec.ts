import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreateBankBranchComponent } from './create-bank-branch.component';
describe('CreateBankBranchComponent', () => {
  let component: CreateBankBranchComponent;
  let fixture: ComponentFixture<CreateBankBranchComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreateBankBranchComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBankBranchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


