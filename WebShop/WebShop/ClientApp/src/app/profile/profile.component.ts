import { Component, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Customer } from '../models/customer.model';
import { LoginService } from '../services/login.service';
import { Cart } from '../models/cart.model';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  customer$: Observable<Customer>;
  carts$: BehaviorSubject<Cart[]>;
  plusBalance: number;

  constructor(private loginService: LoginService, private productService: ProductService) { 
    this.customer$ = this.loginService.customer$;
    this.carts$ = new BehaviorSubject<Cart[]>([]);
    this.plusBalance = 0;
  }

  ngOnInit() {
    this.productService.getCartByUser().subscribe(carts => this.carts$.next(carts));
  }

  uploadBalance(){
    this.productService.uploadBalance(this.plusBalance).subscribe();
  }

}
