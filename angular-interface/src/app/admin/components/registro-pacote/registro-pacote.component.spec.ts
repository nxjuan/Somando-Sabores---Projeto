import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistroPacoteComponent } from './registro-pacote.component';

describe('RegistroPacoteComponent', () => {
  let component: RegistroPacoteComponent;
  let fixture: ComponentFixture<RegistroPacoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegistroPacoteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegistroPacoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
