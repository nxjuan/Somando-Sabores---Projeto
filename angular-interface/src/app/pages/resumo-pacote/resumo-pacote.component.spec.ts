import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumoPacoteComponent } from './resumo-pacote.component';

describe('ResumoPacoteComponent', () => {
  let component: ResumoPacoteComponent;
  let fixture: ComponentFixture<ResumoPacoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ResumoPacoteComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ResumoPacoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
