import { TestBed } from '@angular/core/testing';

import { EmployeeStatusHistoryService } from './employee-status-history.service';

describe('EmployeeStatusHistoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeStatusHistoryService = TestBed.get(EmployeeStatusHistoryService);
    expect(service).toBeTruthy();
  });
});
