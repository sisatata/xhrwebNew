import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOfficeNoticeComponent } from './create-office-notice.component';

describe('CreateOfficeNoticeComponent', () => {
  let component: CreateOfficeNoticeComponent;
  let fixture: ComponentFixture<CreateOfficeNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOfficeNoticeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOfficeNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
