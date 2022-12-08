import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBillClaimComponent } from './create-bill-claim.component';

describe('CreateBillClaimComponent', () => {
  let component: CreateBillClaimComponent;
  let fixture: ComponentFixture<CreateBillClaimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBillClaimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBillClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
