import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  cart: CartItem[];

constructor(private httpService: HttpService) { 
  this.cart = [];
}
  

getProducts(): Observable<Product[]>{
  return this.httpService.getProducts().pipe(map(products => {
    return products.map(product => {
      if(product.image){
        product.image = "data:image/png;base64," + product.image;
      }
      return product;
    })
  }))
}

addItemToCart(product: Product, count: number){
  let productInCart = this.cart.find(c => c.product.id === product.id);
  if(productInCart){
    productInCart.count += count;
  }
  else{
    this.cart.push({product, count});
  }
}

deleteItemFromCart(cartItem: CartItem){
  const index = this.cart.indexOf(cartItem);
  if (index > -1) {
    this.cart.splice(index, 1);
  }
}

}