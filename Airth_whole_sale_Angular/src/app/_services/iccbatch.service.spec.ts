import { TestBed } from '@angular/core/testing';

import { IccbatchService } from './iccbatch.service';



describe('IccbatchService', () => {
  let service: IccbatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IccbatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
