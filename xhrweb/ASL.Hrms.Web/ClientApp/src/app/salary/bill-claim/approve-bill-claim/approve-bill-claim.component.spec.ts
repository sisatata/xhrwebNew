import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveBillClaimComponent } from './approve-bill-claim.component';

describe('ApproveBillClaimComponent', () => {
  let component: ApproveBillClaimComponent;
  let fixture: ComponentFixture<ApproveBillClaimComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveBillClaimComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveBillClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
