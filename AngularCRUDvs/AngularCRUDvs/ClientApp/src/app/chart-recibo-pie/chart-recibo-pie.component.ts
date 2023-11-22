import { Chart, registerables } from 'chart.js';
import { AfterViewInit, Component, Inject, ElementRef, ViewChild, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReciboComponent } from '../recibo/recibo.component';

@Component({
  selector: 'app-chart-recibo-pie',
  templateUrl: './chart-recibo-pie.component.html',
  styleUrls: ['./chart-recibo-pie.component.css']
})
export class ChartReciboPieComponent {

  @Input() item = ''; // decorate the property with @Input()
  @Input() cabeceras: string[] = []; // decorate the property with @Input()
  @Input() totales: { [key: string]: string } = {};

  public cabecerasHidden: string[] = ["UnidadId", "ReciboId", "DPTO", "FechaPago", "MontoPago", "urlVoucher", "nombreVoucher", "TITULAR", "TOTAL MANT","DEUDA ANTERIOR", "TOTAL", "MULTA PAGO FUERA DE FECHA", "CARGO SIGUIENTE MES"];
  public indicesNoDeseadosDefault: number[] = [0, 1, 2, 3, 4, 5, 6, 7, 19, 20, 21, 22, 23];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {



  }


  ngOnInit(): void {

    const dataJson = this.item;
    const cabeceras = this.cabeceras;
    const totales = this.totales;

    const labels = [];
    const values = [];


    for (let col = 0; col < dataJson.length - 1; col++) {
      const columnName = cabeceras[col]; 
      if (columnName) {
        if (!this.shouldHideByIndex(col)) {
          labels.push(columnName);
          const valor = totales[columnName] !== '0' ? totales[columnName] : '0'
          values.push(parseFloat(valor));

        }
          
      }

    }



    const dataCanvas = {
      labels: labels,
      datasets: [{
        label: 'Cantidad',
        data: values,
        backgroundColor: [
          'rgba(105, 250, 132, 0.6)',
          'rgba(54, 162, 235, 0.6)',
          'rgba(255, 206, 86, 0.6)',
          'rgba(75, 192, 192, 0.6)',
          'rgba(153, 90, 5, 0.6)',
          'rgba(33, 22, 64, 0.6)',
          'rgb(255, 99, 132, 0.6)',
          'rgba(5, 120, 92, 0.6)',
          'rgba(133, 133, 50, 0.6)',
          'rgba(100, 9, 4, 0.6)',
          'rgb(55, 99, 32, 0.6)'
        ],
        hoverOffset: cabeceras.length - 1,
      }]
    };

    var myChart = new Chart("myChartPie", {
      type: 'doughnut',
      data: dataCanvas,
      options: {
        plugins: {
          legend: {
            labels: {
              font: {
                size: 8
              }
            }
          }
        }
      }

    });



  }


  shouldHideHeaderByColumnName(column: string): boolean {
    return this.cabecerasHidden.some(condition => column == condition);
  }

  shouldHideByIndex(index: any): boolean {
    return this.indicesNoDeseadosDefault.includes(index);
  }


}
