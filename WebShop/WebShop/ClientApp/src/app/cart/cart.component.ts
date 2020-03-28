import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { CartItem } from '../models/cart-item.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: CartItem[];

  constructor(private productService: ProductService) { 
    this.cart = this.productService.cart;
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
