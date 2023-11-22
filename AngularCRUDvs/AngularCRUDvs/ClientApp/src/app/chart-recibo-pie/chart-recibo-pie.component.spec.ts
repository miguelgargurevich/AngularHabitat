import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartReciboPieComponent } from './chart-recibo-pie.component';

describe('ChartReciboPieComponent', () => {
  let component: ChartReciboPieComponent;
  let fixture: ComponentFixture<ChartReciboPieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChartReciboPieComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChartReciboPieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
