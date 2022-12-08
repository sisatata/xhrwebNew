import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateIncomeTaxSlabComponent } from './create-income-tax-slab.component';

describe('CreateIncomeTaxSlabComponent', () => {
  let component: CreateIncomeTaxSlabComponent;
  let fixture: ComponentFixture<CreateIncomeTaxSlabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateIncomeTaxSlabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateIncomeTaxSlabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
