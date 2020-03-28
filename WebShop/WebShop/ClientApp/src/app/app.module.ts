import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
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
import { ProductCardComponent } from './product/product-card/product-card.component';
import { CartItemComponent } from './product/cart-item/cart-item.component';
import { CartComponent } from './cart/cart.component';
import { UploadProductComponent } from './product/upload-product/upload-product.component';
import { SingleProductComponent } from './product/single-product/single-product.component';
import { ImagePipe } from './pipes/image.pipe';
import { CommentComponent } from './product/comment/comment.component';

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      CounterComponent,
      FetchDataComponent,
      LoginComponent,
      ProductComponent,
      RegistrationComponent,
      ProductCardComponent,
      CartItemComponent,
      CartComponent,
      UploadProductComponent,
      SingleProductComponent,
      ImagePipe,
      CommentComponent
   ],
   imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ProductComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'products/:id', component: SingleProductComponent, canActivate: [AuthGuard] },
      { path: 'products', component: ProductComponent, canActivate: [AuthGuard] },
      { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
      { path: 'uploadProduct', component: UploadProductComponent, canActivate: [AuthGuard] },
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
