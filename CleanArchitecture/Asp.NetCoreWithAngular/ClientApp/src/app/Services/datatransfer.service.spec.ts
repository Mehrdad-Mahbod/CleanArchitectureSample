import { TestBed } from '@angular/core/testing';

import { DataTransferService } from '../Services/datatransfer.service';

describe('DatatransferService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DataTransferService = TestBed.get(DataTransferService);
    expect(service).toBeTruthy();
  });
});
