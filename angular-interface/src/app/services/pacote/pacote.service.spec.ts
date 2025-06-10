import { TestBed } from '@angular/core/testing';

import { PacoteService } from './pacote.service';

describe('PacoteService', () => {
  let service: PacoteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PacoteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
