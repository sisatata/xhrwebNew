import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateShiftAllocationComponent } from './create-shift-allocation.component';

describe('CreateShiftAllocationComponent', () => {
  let component: CreateShiftAllocationComponent;
  let fixture: ComponentFixture<CreateShiftAllocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateShiftAllocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateShiftAllocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
