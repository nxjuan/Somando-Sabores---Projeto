import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CauroselComponent } from './caurosel.component';

describe('CauroselComponent', () => {
  let component: CauroselComponent;
  let fixture: ComponentFixture<CauroselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CauroselComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CauroselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
