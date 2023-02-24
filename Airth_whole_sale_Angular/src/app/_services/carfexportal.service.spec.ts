import { TestBed } from '@angular/core/testing';

import { CarfexportalService } from './carfexportal.service';

describe('CarfexportalService', () => {
  let service: CarfexportalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarfexportalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
