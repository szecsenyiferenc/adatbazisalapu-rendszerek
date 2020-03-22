import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { switchMap, tap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.css']
})
export class SingleProductComponent implements OnInit {
  product$: Observable<Product>;
  product: Product;
  counter: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) { 
    this.product$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.productService.getProductById(params.get('id'))),
      tap(product => this.product = product),
      catchError(() => {
        this.router.navigate(['/products']);
        return of(null);
      })
    );
    this.counter = 1;
  }

  ngOnInit() {
  }

  addToCart(){
    this.productService.addItemToCart(this.product, this.counter);
  }

}
