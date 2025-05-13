import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroPagamentoComponent } from './registro-pagamento.component';

describe('RegistroPagamentoComponent', () => {
  let component: RegistroPagamentoComponent;
  let fixture: ComponentFixture<RegistroPagamentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistroPagamentoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
