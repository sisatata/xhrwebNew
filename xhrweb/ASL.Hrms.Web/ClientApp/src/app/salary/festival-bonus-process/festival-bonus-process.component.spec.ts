import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FestivalBonusProcessComponent } from './festival-bonus-process.component';

describe('FestivalBonusProcessComponent', () => {
  let component: FestivalBonusProcessComponent;
  let fixture: ComponentFixture<FestivalBonusProcessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FestivalBonusProcessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FestivalBonusProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
