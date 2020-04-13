import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { ProductService } from '../services/product.service';
import { CartItem } from '../models/cart-item.model';
import { Observable, BehaviorSubject } from 'rxjs';
import { Category } from '../models/category.model';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  cartItems: CartItem[];
  categories$: BehaviorSubject<Category[]>;

  constructor(private loginService: LoginService, private productService: ProductService){
    this.cartItems = this.productService.cart;
    this.categories$ = new BehaviorSubject<Category[]>([]);
  }

  ngOnInit(){
    this.productService.getCategories().pipe(tap(categories => this.categories$.next(categories))).subscribe();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout(){
    this.loginService.logout();
  }
}
