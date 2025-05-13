import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroReservaComponent } from './registro-reserva.component';

describe('RegistroReservaComponent', () => {
  let component: RegistroReservaComponent;
  let fixture: ComponentFixture<RegistroReservaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistroReservaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroReservaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
