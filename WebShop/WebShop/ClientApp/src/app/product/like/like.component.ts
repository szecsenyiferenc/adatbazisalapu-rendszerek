import { Component, OnInit, Input } from '@angular/core';
import { LoginService } from 'src/app/services/login.service';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { Customer } from 'src/app/models/customer.model';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { debounceTime, distinctUntilChanged, switchMap, map, tap } from 'rxjs/operators';
import { Like } from 'src/app/models/like.model';

@Component({
  selector: 'app-like',
  templateUrl: './like.component.html',
  styleUrls: ['./like.component.css']
})
export class LikeComponent implements OnInit {
  @Input() like: boolean;
  @Input() product: Product;

  likeProduct$: Observable<boolean>;
  likeSubject$: BehaviorSubject<boolean>;
  customer$: Observable<Customer>;

  constructor(private loginService: LoginService, private productService: ProductService) {
    this.customer$ = this.loginService.customer$;
  }

  ngOnInit() {
  }

  setLike(value: boolean){
    if(this.like !== null && this.like && value){
      this.like = null;
    }
    else if(this.like !== null && !this.like && !value){
      this.like = null;
    }
    else {
      this.like = value;
    }
    this.sendLike();
  }

  sendLike(){
    const like: Like = {
      customer: this.loginService.customer$.value,
      product: this.product,
      value: this.like
    }
    this.productService.setLike(like).subscribe(a => console.log(a));
  }

}
