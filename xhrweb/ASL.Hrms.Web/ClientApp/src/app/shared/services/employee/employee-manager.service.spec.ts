import { TestBed } from '@angular/core/testing';

import { EmployeeManagerService } from './employee-manager.service';

describe('EmployeeManagerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeManagerService = TestBed.get(EmployeeManagerService);
    expect(service).toBeTruthy();
  });
});
