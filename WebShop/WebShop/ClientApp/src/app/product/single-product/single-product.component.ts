import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { switchMap, tap, catchError, map } from 'rxjs/operators';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { ProductComment } from 'src/app/models/comment.model';
import { LoginService } from 'src/app/services/login.service';
import { LikedProduct } from 'src/app/models/likedProduct.model';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.css']
})
export class SingleProductComponent implements OnInit {
  product$: Observable<LikedProduct>;
  product: Product;
  counter: number;
  comments$: BehaviorSubject<ProductComment[]> = new BehaviorSubject<ProductComment[]>(null);
  getComments$: Observable<ProductComment[]>;
  customerComment: ProductComment;
  commentField: string;
  like?:boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private loginService: LoginService
  ) { 
    this.product$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.productService.getProductById(params.get('id'))),
      tap(product => this.product = product),
      catchError(() => {
        this.router.navigate(['/products']);
        return of(null);
      })
    );

    this.customerComment = {
      customer: this.loginService.customer$.value
    }

    this.getComments$ = this.product$.pipe(
      switchMap(product => this.productService.getAllComment(product)),
      map(product => {
        if(product){
          return product.sort((a, b) => (a.dateTime < b.dateTime) ? 1 : -1);
        }
        return [];
      }),
    );

    this.counter = 1;
    this.like = null;
  }

  ngOnInit() {    
    this.getComments$.subscribe(comments => this.comments$.next(comments));
  }

  reload(){
    this.getComments$.subscribe(comments => this.comments$.next(comments));
  }

  addToCart(){
    this.productService.addItemToCart(this.product, this.counter);
  }

}
