import { TestBed } from '@angular/core/testing';

import { LoadmenusService } from './loadmenus.service';

describe('LoadmenusService', () => {
  let service: LoadmenusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoadmenusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
