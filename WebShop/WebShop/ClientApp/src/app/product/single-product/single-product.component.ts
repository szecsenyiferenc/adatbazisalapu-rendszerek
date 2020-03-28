import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { switchMap, tap, catchError, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { ProductComment } from 'src/app/models/comment.model';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.css']
})
export class SingleProductComponent implements OnInit {
  product$: Observable<Product>;
  product: Product;
  counter: number;
  comments$: Observable<ProductComment[]>;
  comments: ProductComment[];
  customerComment: ProductComment;
  commentField: string;

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

    this.counter = 1;
  }

  ngOnInit() {    
    this.product$.pipe(
      switchMap(product => this.productService.getAllComment(product)),
      map(product => product.sort((a, b) => (a.dateTime < b.dateTime) ? 1 : -1))
    ).subscribe(comments => this.comments = comments);
  }

  reload(){
    this.product$.pipe(
      switchMap(product => this.productService.getAllComment(product)),
      map(product => product.sort((a, b) => (a.dateTime < b.dateTime) ? 1 : -1))
    ).subscribe(comments => this.comments = comments);
  }

  addToCart(){
    this.productService.addItemToCart(this.product, this.counter);
  }

}
