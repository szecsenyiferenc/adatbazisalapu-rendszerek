import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { CartItem } from '../models/cart-item.model';
import { Customer } from '../models/customer.model';
import { LoginService } from '../services/login.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: CartItem[];
  customer$: Observable<Customer>;
  fullAmount: number;

  constructor(private productService: ProductService, private loginService: LoginService) { 
    this.cart = this.productService.cart;
    this.customer$ = this.loginService.customer$;
    this.fullAmount = 0;
  }

  ngOnInit() {
  }

  get totalPrice(){
    let price = 0;
      this.cart.forEach(element => {
        price += element.quantity*element.product.price;
      });
    return price;
  }

  multiplyQuantity(quantity: number, price: number){
    return quantity*price;
  }

  deleteItemFromCart(cartItem: CartItem){
    this.productService.deleteItemFromCart(cartItem);
  }

  orderProducts(){
    console.log(this.cart);
    this.productService.uploadCart(this.cart).subscribe(a => console.log(a));
  }

}
