import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdmPacotesComponent } from './adm-pacotes.component';

describe('AdmPacotesComponent', () => {
  let component: AdmPacotesComponent;
  let fixture: ComponentFixture<AdmPacotesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdmPacotesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdmPacotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
