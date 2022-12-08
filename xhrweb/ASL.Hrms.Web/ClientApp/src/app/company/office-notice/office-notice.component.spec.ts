import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficeNoticeComponent } from './office-notice.component';

describe('OfficeNoticeComponent', () => {
  let component: OfficeNoticeComponent;
  let fixture: ComponentFixture<OfficeNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfficeNoticeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OfficeNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
