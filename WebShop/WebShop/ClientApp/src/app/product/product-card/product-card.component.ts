import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { Router } from '@angular/router';
import { LikedProduct } from 'src/app/models/likedProduct.model';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: LikedProduct;

  counter: number;

  constructor(private router: Router, private productService: ProductService) {
    this.counter = 1;
   }

  ngOnInit() {

  }

  addToCart(){
    this.productService.addItemToCart(this.product, this.counter);
  }

  openProduct(){
    this.router.navigate(['/products', this.product.id]);
  }
}
