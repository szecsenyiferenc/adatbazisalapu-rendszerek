import { Component, OnInit } from '@angular/core';
import { Observable, BehaviorSubject, pipe } from 'rxjs';
import { Customer } from '../models/customer.model';
import { LoginService } from '../services/login.service';
import { Cart } from '../models/cart.model';
import { ProductService } from '../services/product.service';
import { VisitedProduct } from '../models/visitedProduct.model';
import { tap, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  customer$: Observable<Customer>;
  carts$: BehaviorSubject<Cart[]>;
  visitedProducts$: BehaviorSubject<VisitedProduct[]>;
  plusBalance: number;

  constructor(private loginService: LoginService, private productService: ProductService, private router: Router) { 
    this.customer$ = this.loginService.customer$;
    this.carts$ = new BehaviorSubject<Cart[]>([]);
    this.visitedProducts$ = new BehaviorSubject<VisitedProduct[]>([]);
    this.plusBalance = 0;
  }

  ngOnInit() {
    this.productService.getCartByUser().subscribe(carts => this.carts$.next(carts));
    this.productService.getVisitedProducts().pipe(
      map(products => products.filter(p => p.timesOfVisit !== 0)
      .sort((a,b) => a.timesOfVisit > b.timesOfVisit ? -1 : 1))
    ).subscribe(visited => this.visitedProducts$.next(visited));

    this.visitedProducts$.pipe(tap(a => console.log(a))).subscribe();
  }

  uploadBalance(){
    this.productService.uploadBalance(this.plusBalance).subscribe();
  }

  update(){
    this.router.navigateByUrl('/registration');
  }

  delete(){
    this.loginService.delete();
    this.router.navigateByUrl('/products');
  }


}
