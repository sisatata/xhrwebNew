import { TestBed } from '@angular/core/testing';

import { EmployeeStatusService } from './employee-status.service';

describe('EmployeeStatusService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeStatusService = TestBed.get(EmployeeStatusService);
    expect(service).toBeTruthy();
  });
});
