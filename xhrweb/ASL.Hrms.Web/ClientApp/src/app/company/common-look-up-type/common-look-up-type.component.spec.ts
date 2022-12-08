import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonLookUpTypeComponent } from './common-look-up-type.component';

describe('CommonLookUpTypeComponent', () => {
  let component: CommonLookUpTypeComponent;
  let fixture: ComponentFixture<CommonLookUpTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommonLookUpTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommonLookUpTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
