
import { Chart, registerables } from 'chart.js';
import { AfterViewInit, Component, Inject, ElementRef, ViewChild, HostListener } from '@angular/core';
import { HttpClient } from '@angular/common/http';

Chart.register(...registerables);

@Component({
  selector: 'app-recibo',
  templateUrl: './recibo.component.html',
  styleUrls: ['./recibo.component.css']
})


export class ReciboComponent {

  public model!: DataTableListModel;
  public modelList: DataTableListModel[] = [];
  public user!: UsersModel;
  public recibo!: Recibo; 
  public recibos: Recibo[] = [];
  public cabeceras: string[] = [];
  public totales: { [key: string]: string } = {};
  public matrizObj: string = '';
  public mesesList: number[] = [];
  public cabecerasHidden: string[] = ["UnidadId", "ReciboId", "FechaPago", "MontoPago", "urlVoucher", "nombreVoucher", "TITULAR", "TOTAL MANT", "MULTA PAGO FUERA DE FECHA", "CARGO SIGUIENTE MES"];
  public indicesNoDeseadosDefault: number[] = [0, 1, 3, 4, 5, 6, 7, 19, 22, 23];
  public indicesNoDeseadosMaxWith: number[] = [8,9,10,11,12,13,14,15,16,17,18,20];

  innerWidth: number = 0;
  innerHeight: number = 0;
  columnsToDisplay: number[] = [];
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {


    http.get<DataTableListModel[]>(baseUrl + 'recibo').subscribe(result => {
      this.modelList = result;
      this.model = this.modelList[0];

      this.user = this.model.user;
      this.recibos = this.model.recibos;
      this.recibo = this.model.recibo;
      this.cabeceras = this.model.cabeceras;
      this.totales = this.model.totales;
      this.mesesList = this.model.mesesList;
      this.matrizObj = JSON.parse(this.model.matrizString);
      
      this.columnsToDisplay = this.getVisibleColumns(this.innerWidth);

    }, error => console.error(error));

  }


  getVisibleColumns(screenWidth: number): number[] {
    let visibleColumns: number[];
  
    if (screenWidth < 768) {
      visibleColumns = [...this.indicesNoDeseadosDefault, ...this.indicesNoDeseadosMaxWith];
    } else {
      visibleColumns = [...this.indicesNoDeseadosDefault];
    }
  
    return visibleColumns;
  }

  // Escucha el evento de cambio de tamaño de la ventana
  @HostListener('window:resize', ['$event'])
  onResize(event: any): void {
    const screenWidth = event.target.innerWidth;
    this.columnsToDisplay = this.getVisibleColumns(screenWidth);
  }
  

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.innerHeight - 100; 
  }


  shouldHideHeaderByColumnName(column: string): boolean {
    return this.cabecerasHidden.some(condition => column == condition);
  }

  shouldHideByIndex(index: any): boolean {
    let rpta: boolean = false;
  
    if (this.columnsToDisplay.includes(index)) {
      rpta = true;
    }
  
    return rpta;
  }


  private calculateFooterPosition(): void {


    const anchoVentana = window.innerWidth;
    const containerTable = document.getElementById('containerTable');
    
    if (containerTable) {
    //  //const containerTableTop = containerTable.getBoundingClientRect().height;
    //  //let altura = containerTableTop; // window.outerHeight;


      let altura = containerTable.clientHeight;

      //console.log('Altura del elemento:', altura);

      if (anchoVentana <= 755) {
        altura = 900 + 1200;
      } else {
        altura = 900 + 1200;
      }

      const footer = document.getElementById('footer');
      //console.log("footer", footer);
      if (footer) {
        //this.renderer.setStyle(footer, 'top', `${altura}px`);
        //this.renderer.setStyle(footer, 'position', 'absolute');
        //footer.style.display = 'none';
        //footer.style.top = `${altura}px`;
        //footer.style.position = "absolute";


      }


    }

    
  }

}

interface UsersModel {
    userId: number;
    userName?: string;
    passwordHash?: string;
    estado?: boolean;
    role?: string;
    unidadId: number;
    mantenerActivo: boolean;
}

interface Recibo {
  reciboId: number;
  descripcion: string;
  fechaEmision: Date;
  fechaVencimiento: Date;
  mes: number;
  anio: number;
  estado?: string;
  fechaEmisionStr: string;
  fechaVencimientoStr: string;
}

interface DataTableListModel {
  user: UsersModel;
  cabeceras: string[];
  mesesList: number[];
  //aniosList?: number[];
  ////Matriz?: string[][];
  matrizString: string;
  totales: { [key: string]: string };
  recibos: Recibo[];
  recibo: Recibo;
}



