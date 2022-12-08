import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateShiftGroupComponent } from './create-shift-group.component';

describe('CreateShiftGroupComponent', () => {
  let component: CreateShiftGroupComponent;
  let fixture: ComponentFixture<CreateShiftGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateShiftGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateShiftGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
