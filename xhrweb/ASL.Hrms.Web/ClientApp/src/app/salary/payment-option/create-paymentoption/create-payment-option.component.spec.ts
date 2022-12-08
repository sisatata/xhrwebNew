import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CreatePaymentOptionComponent } from './create-payment-option.component';
describe('CreatePaymentOptionComponent', () => {
  let component: CreatePaymentOptionComponent;
  let fixture: ComponentFixture<CreatePaymentOptionComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CreatePaymentOptionComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePaymentOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


