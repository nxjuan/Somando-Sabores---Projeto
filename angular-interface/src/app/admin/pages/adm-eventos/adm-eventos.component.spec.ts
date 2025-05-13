import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmEventosComponent } from './adm-eventos.component';

describe('AdmEventosComponent', () => {
  let component: AdmEventosComponent;
  let fixture: ComponentFixture<AdmEventosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmEventosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmEventosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
