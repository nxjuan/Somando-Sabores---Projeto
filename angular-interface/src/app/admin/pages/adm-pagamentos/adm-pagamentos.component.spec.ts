import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmPagamentosComponent } from './adm-pagamentos.component';

describe('AdmPagamentosComponent', () => {
  let component: AdmPagamentosComponent;
  let fixture: ComponentFixture<AdmPagamentosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmPagamentosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmPagamentosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
