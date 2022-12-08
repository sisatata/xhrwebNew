import { TestBed } from '@angular/core/testing';

import { EmployeePhoneService } from './employee-phone.service';

describe('EmployeePhoneService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeePhoneService = TestBed.get(EmployeePhoneService);
    expect(service).toBeTruthy();
  });
});
