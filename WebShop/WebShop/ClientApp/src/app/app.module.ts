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
import { LoginComponent } from './login/login.component';
import { HttpService } from './services/http.service';
import { LoginService } from './services/login.service';
import { ProductComponent } from './product/product.component';
import { ProductService } from './services/product.service';
import { AuthGuard } from './guards/auth.guard';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      CounterComponent,
      FetchDataComponent,
      LoginComponent,
      ProductComponent,
      RegistrationComponent
   ],
   imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'products', component: ProductComponent, canActivate: [AuthGuard] }
    ])
  ],
  providers: [
    HttpService,
    LoginService,
    ProductService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
