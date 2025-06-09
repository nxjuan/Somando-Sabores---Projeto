import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmRelatoriosComponent } from './adm-relatorios.component';

describe('AdmRelatoriosComponent', () => {
  let component: AdmRelatoriosComponent;
  let fixture: ComponentFixture<AdmRelatoriosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmRelatoriosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmRelatoriosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
