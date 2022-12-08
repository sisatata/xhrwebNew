import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EarnLeaveEncashmentSetupComponent } from './earn-leave-encashment-setup.component';

describe('EarnLeaveEncashmentSetupComponent', () => {
  let component: EarnLeaveEncashmentSetupComponent;
  let fixture: ComponentFixture<EarnLeaveEncashmentSetupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EarnLeaveEncashmentSetupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EarnLeaveEncashmentSetupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
