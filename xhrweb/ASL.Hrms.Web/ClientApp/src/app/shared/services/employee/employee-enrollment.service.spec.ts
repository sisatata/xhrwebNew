import { TestBed } from '@angular/core/testing';

import { EmployeeEnrollmentService } from './employee-enrollment.service';

describe('EmployeeEnrollmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeEnrollmentService = TestBed.get(EmployeeEnrollmentService);
    expect(service).toBeTruthy();
  });
});
