import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveOrRejectEmployeePromotionTransferComponent } from './approve-or-reject-employee-promotion-transfer.component';

describe('ApproveOrRejectEmployeePromotionTransferComponent', () => {
  let component: ApproveOrRejectEmployeePromotionTransferComponent;
  let fixture: ComponentFixture<ApproveOrRejectEmployeePromotionTransferComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveOrRejectEmployeePromotionTransferComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveOrRejectEmployeePromotionTransferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
