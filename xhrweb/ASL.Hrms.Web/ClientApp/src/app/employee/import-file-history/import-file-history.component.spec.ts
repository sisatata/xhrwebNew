import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportFileHistoryComponent } from './import-file-history.component';

describe('ImportFileHistoryComponent', () => {
  let component: ImportFileHistoryComponent;
  let fixture: ComponentFixture<ImportFileHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ImportFileHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportFileHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
