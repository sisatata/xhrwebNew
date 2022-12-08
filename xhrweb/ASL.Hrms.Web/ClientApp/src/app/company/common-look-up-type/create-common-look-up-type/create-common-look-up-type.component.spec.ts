import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCommonLookUpTypeComponent } from './create-common-look-up-type.component';

describe('CreateCommonLookUpTypeComponent', () => {
  let component: CreateCommonLookUpTypeComponent;
  let fixture: ComponentFixture<CreateCommonLookUpTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateCommonLookUpTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateCommonLookUpTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
