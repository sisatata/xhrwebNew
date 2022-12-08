import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeePromotionTransferListComponent } from './employee-promotion-transfer-list.component';

describe('EmployeePromotionTransferListComponent', () => {
  let component: EmployeePromotionTransferListComponent;
  let fixture: ComponentFixture<EmployeePromotionTransferListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeePromotionTransferListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeePromotionTransferListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
