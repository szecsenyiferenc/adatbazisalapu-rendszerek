import { Component, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { ProductService } from '../services/product.service';
import { LikedProduct } from '../models/likedProduct.model';
import { tap, switchMap, map, filter } from 'rxjs/operators';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products$: BehaviorSubject<LikedProduct[]>;
  products: LikedProduct[];

  constructor(private productService: ProductService) {
    this.products$ = new BehaviorSubject<LikedProduct[]>([]);
   }

  ngOnInit() {
     this.productService.getProducts().pipe(
       tap(products => this.products$.next(products)),
       switchMap(() => this.productService.getLikes()),
       map((likes: any[]) => {
         if(likes) {
          return this.products$.value.map(product => {
            const likeObject = likes.find(element => element.productId === product.id);
            product.like = likeObject && likeObject.value;
            if(product.like === undefined){
              product.like = null;
            }
            return product;
         })};
         return this.products$.value;
        }),
       map(p => p.sort((a,b) => a.likes > b.likes ? -1 : 1)),
       tap(products => this.products$.next(products)),
       tap(products => this.products = products)
     ).subscribe()

     this.productService.selectedCategory$.pipe(
      map(category => {

        if(category){
            return this.products.filter(p => p.categories.find(c => c.id === category.id))
        }
      
        return this.products;
      }),
      tap(products => this.products$.next(products)),
     ).subscribe();

     this.products$.pipe(tap(products => console.log("PRODUCTS", products))).subscribe();
  }

}
