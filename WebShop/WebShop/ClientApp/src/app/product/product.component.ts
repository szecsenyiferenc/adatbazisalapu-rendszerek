import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../services/http.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products$: Observable<any>

  constructor(private httpService: HttpService) { }

  ngOnInit() {
    this.products$ = this.httpService.getProducts();
  }

}
