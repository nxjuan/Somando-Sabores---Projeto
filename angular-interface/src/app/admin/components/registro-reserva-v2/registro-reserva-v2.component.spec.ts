import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroReservaV2Component } from './registro-reserva-v2.component';

describe('RegistroReservaV2Component', () => {
  let component: RegistroReservaV2Component;
  let fixture: ComponentFixture<RegistroReservaV2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistroReservaV2Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroReservaV2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
