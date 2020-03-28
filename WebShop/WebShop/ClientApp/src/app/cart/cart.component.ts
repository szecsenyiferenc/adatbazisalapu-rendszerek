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

  constructor(private productService: ProductService, private loginService: LoginService) { 
    this.cart = this.productService.cart;
    this.customer$ = this.loginService.customer$;
  }

  ngOnInit() {
  }

  deleteItemFromCart(cartItem: CartItem){
    this.productService.deleteItemFromCart(cartItem);
  }

  orderProducts(){
    console.log(this.cart);
    this.productService.uploadCart(this.cart).subscribe(a => console.log(a));
  }

}
