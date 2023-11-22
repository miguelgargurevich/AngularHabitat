import { Chart, registerables } from 'chart.js';
import { AfterViewInit, Component, Inject, ElementRef, ViewChild, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReciboComponent } from '../recibo/recibo.component';

@Component({
  selector: 'app-chart-recibo-barras',
  templateUrl: './chart-recibo-barras.component.html',
  styleUrls: ['./chart-recibo-barras.component.css']
})
export class ChartReciboBarrasComponent {

  @Input() item = ''; // decorate the property with @Input()

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {



  }


  ngOnInit(): void {

    const dataJson = this.item;

    const labels = [];
    const values = [];

    for (let i = 0; i < dataJson.length; i++) {
      const item = dataJson[i];
      const dpto = item[2];
      const total = parseFloat(item[item.length - 3]);
      labels.push(dpto);
      values.push(total);

    }

    const dataCanvas = {
      labels: labels,
      datasets: [
        {
          label: 'DPTO',
          data: values,
          //backgroundColor: '#FFABAB',
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)',
            'rgba(54, 162, 235, 0.2)',
            'rgba(255, 206, 86, 0.2)',
            'rgba(75, 192, 192, 0.2)',
            'rgba(153, 102, 255, 0.2)',
            'rgba(255, 159, 64, 0.2)'
          ],
          borderColor: [
            'rgba(255, 99, 132, 1)',
            'rgba(54, 162, 235, 1)',
            'rgba(255, 206, 86, 1)',
            'rgba(75, 192, 192, 1)',
            'rgba(153, 102, 255, 1)',
            'rgba(255, 159, 64, 1)'
          ],
          borderWidth: 1,
        },
      ],
    };

    var myChart = new Chart("myChartBarras", {
      type: 'bar',
      data: dataCanvas,
      options: {
        scales: {
          y: {
            beginAtZero: true,
            ticks: {
              font: {
                size: 12,
              },
            },
          },
          x: {
            ticks: {
              font: {
                size: 12,
              },
            },
          },
        },
        plugins: {
          legend: {
            labels: {
              font: {
                size: 12,
              },
            },
          },
        },
      },
    });





  }


}
