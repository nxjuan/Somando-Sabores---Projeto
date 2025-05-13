import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroEventoComponent } from './registro-evento.component';

describe('RegistroEventoComponent', () => {
  let component: RegistroEventoComponent;
  let fixture: ComponentFixture<RegistroEventoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistroEventoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroEventoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
