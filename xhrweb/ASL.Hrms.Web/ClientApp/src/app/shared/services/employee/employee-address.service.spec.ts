import { TestBed } from '@angular/core/testing';

import { EmployeeAddressService } from './employee-address.service';

describe('EmployeeAddressService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EmployeeAddressService = TestBed.get(EmployeeAddressService);
    expect(service).toBeTruthy();
  });
});
