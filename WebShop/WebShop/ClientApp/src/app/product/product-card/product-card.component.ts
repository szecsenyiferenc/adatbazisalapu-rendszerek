import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() product: Product;
  counter: number;

  constructor(private productService: ProductService) {
    this.counter = 1;
   }

  ngOnInit() {
  }

  addToCart(){
    this.productService.addItemToCart(this.product, this.counter);
  }



}
