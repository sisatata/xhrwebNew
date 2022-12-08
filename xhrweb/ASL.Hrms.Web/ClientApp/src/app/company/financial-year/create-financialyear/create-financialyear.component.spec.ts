import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFinancialyearComponent } from './create-financialyear.component';

describe('CreateFinancialyearComponent', () => {
  let component: CreateFinancialyearComponent;
  let fixture: ComponentFixture<CreateFinancialyearComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateFinancialyearComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateFinancialyearComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
