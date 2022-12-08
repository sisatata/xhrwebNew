import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyDetailBaseComponent } from './company-detail-base.component';

describe('CompanyDetailBaseComponent', () => {
  let component: CompanyDetailBaseComponent;
  let fixture: ComponentFixture<CompanyDetailBaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyDetailBaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyDetailBaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
