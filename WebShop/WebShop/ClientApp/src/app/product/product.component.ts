import { Component, OnInit } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { ProductService } from '../services/product.service';
import { LikedProduct } from '../models/likedProduct.model';
import { tap, switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products$: BehaviorSubject<LikedProduct[]>;

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
       tap(products => this.products$.next(products)),
     ).subscribe()

     this.products$.pipe(tap(products => console.log(products))).subscribe();
  }

}
