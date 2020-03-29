import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ProductComment } from 'src/app/models/comment.model';
import { ProductService } from 'src/app/services/product.service';
import { LoginService } from 'src/app/services/login.service';
import { Product } from 'src/app/models/product.model';
import { Router, ActivatedRoute } from '@angular/router';
import { tap, map } from 'rxjs/operators';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() comment: ProductComment;
  @Input() isNewComment: boolean;
  @Input() product: Product;
  @Output() reload = new EventEmitter();
  commentField: string;

  constructor(private productService: ProductService, private loginService: LoginService, private route: ActivatedRoute ,private router: Router) { }

  ngOnInit() {
  }

  sendComment(){
    const comment: ProductComment = {
      customer: this.loginService.customer$.value,
      product: this.product,
      dateTime: new Date(),
      text: this.commentField
    }

    this.productService.uploadComment(comment)
    .pipe(map(() => this.reloadComments()))
    .subscribe();
  }

  
  reloadComments() { 
    this.reload.emit();
}


}
