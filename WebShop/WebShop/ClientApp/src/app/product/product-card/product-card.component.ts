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

  stock: number;
  counter: number;

  constructor(private router: Router, private productService: ProductService) {
    this.counter = 1;
   }

  ngOnInit() {

  }

  addToCart(){
    this.productService.getStockByProduct(this.product).subscribe(a => {
      this.stock = a;
      if (this.stock >= this.counter){
        this.productService.addItemToCart(this.product, this.counter);
      } else {
        alert('Nincs elegendő készleten. Jelenlegi készlet: ' + this.stock);
      }
      }
    );
  }

  openProduct(){
    this.productService.addToVisitedProduct(this.product);
    this.router.navigate(['/products', this.product.id]);
  }
}
