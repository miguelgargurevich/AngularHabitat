import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartReciboBarrasComponent } from './chart-recibo-barras.component';

describe('ChartReciboBarrasComponent', () => {
  let component: ChartReciboBarrasComponent;
  let fixture: ComponentFixture<ChartReciboBarrasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChartReciboBarrasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChartReciboBarrasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
