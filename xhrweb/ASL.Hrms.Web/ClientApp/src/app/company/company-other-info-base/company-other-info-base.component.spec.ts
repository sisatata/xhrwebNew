import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyOtherInfoBaseComponent } from './company-other-info-base.component';

describe('CompanyOtherInfoBaseComponent', () => {
  let component: CompanyOtherInfoBaseComponent;
  let fixture: ComponentFixture<CompanyOtherInfoBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyOtherInfoBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyOtherInfoBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
