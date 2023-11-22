import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ReciboComponent } from './recibo/recibo.component';
import { MyChartComponent } from './my-chart/my-chart.component';
import { ChartReciboBarrasComponent } from './chart-recibo-barras/chart-recibo-barras.component';
import { ChartReciboPieComponent } from './chart-recibo-pie/chart-recibo-pie.component';

//import { PersonasComponent } from './personas/personas.component';
//import { PersonasService } from './personas/personas.service';
//import { PersonasFormComponent } from './personas/personas-form/personas-form.component';
//import { DireccionesService } from './direcciones/direcciones.service';
//import { LeaveFormService } from './personas/personas-form/leave-form.service';
//import { LogInterceptorService } from './services/log-interceptor.service';
//import { RegisterComponent } from './account/register/register.component';
//import { AuthGuardService } from './services/auth-guard.service';
//import { AccountService } from './account/account.service';
//import { AuthInterceptorService } from './services/auth-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ReciboComponent,
    MyChartComponent,
    ChartReciboBarrasComponent,
    ChartReciboPieComponent,
    //RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'recibo', component: ReciboComponent },
      { path: 'my-chart', component: MyChartComponent },
      //{ path: 'personas', component: PersonasComponent, canActivate: [AuthGuardService] },
      //{ path: 'personas-agregar', component: PersonasFormComponent, canDeactivate: [LeaveFormService] },
      //{ path: 'personas-editar/:id', component: PersonasFormComponent, canDeactivate: [LeaveFormService] },
    //  { path: 'register-login', component: RegisterComponent }
    ])
  ],
  providers:[],
  //providers: [PersonasService,
  //  //DireccionesService,
  //  //LeaveFormService,
  //  AuthGuardService,
  //  AccountService,
  //  {
  //    provide: HTTP_INTERCEPTORS,
  //    useClass: LogInterceptorService,
  //    multi: true
  //  },
  //  {
  //    provide: HTTP_INTERCEPTORS,
  //    useClass: AuthInterceptorService,
  //    multi: true
  //  }
  //],
  bootstrap: [AppComponent]
})
export class AppModule { }

