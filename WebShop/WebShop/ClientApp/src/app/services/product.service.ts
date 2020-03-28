import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { map, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { LoginService } from './login.service';
import { Cart } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  cart: CartItem[];
  products: Product[];

constructor(private httpService: HttpService, private loginService: LoginService) { 
  this.cart = [];
}
  

getProducts(): Observable<Product[]>{
  return this.httpService.getProducts().pipe(tap(products => this.products = products));
}

getProductById(id: string): Observable<Product>{
  let selectedProduct = this.products.find(p => p.id === +id);
  return of(selectedProduct);
}

addItemToCart(product: Product, quantity: number){
  let productInCart = this.cart.find(c => c.product.id === product.id);
  if(productInCart){
    productInCart.quantity += quantity;
  }
  else{
    this.cart.push({product, quantity});
  }
}

deleteItemFromCart(cartItem: CartItem){
  const index = this.cart.indexOf(cartItem);
  if (index > -1) {
    this.cart.splice(index, 1);
  }
}

uploadProduct(product: Product): Observable<any>{
  return this.httpService.uploadProduct(product);
}

uploadCart(cartItems: CartItem[]): Observable<any>{
  const cart: Cart = {
    customer: this.loginService.customer$.value,
    cartItems
  }
  return this.httpService.uploadCart(cart);
}

}