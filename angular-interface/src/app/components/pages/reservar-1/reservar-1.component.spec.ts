import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Reservar1Component } from './reservar-1.component';

describe('Reservar1Component', () => {
  let component: Reservar1Component;
  let fixture: ComponentFixture<Reservar1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Reservar1Component]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Reservar1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
