import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalaryStructureComponentComponent } from './salary-structure-component.component';

describe('SalaryStructureComponentComponent', () => {
  let component: SalaryStructureComponentComponent;
  let fixture: ComponentFixture<SalaryStructureComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalaryStructureComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalaryStructureComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

