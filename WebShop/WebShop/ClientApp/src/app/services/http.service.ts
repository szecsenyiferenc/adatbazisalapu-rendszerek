import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginData } from '../models/login-data.model';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { tap } from 'rxjs/operators';
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { Cart } from '../models/cart.model';
import { ProductComment } from '../models/comment.model';
import { Like } from '../models/like.model';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private baseUrl = document.getElementsByTagName('base')[0].href;

  constructor(private http: HttpClient) { }

  checkLogin(loginData: LoginData): Observable<any> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/login', JSON.stringify(loginData), { headers });
  }

  getProducts(): Observable<any> {
    return this.http.get(this.baseUrl + 'api/product');
  }

  getVisitedProducts(email: string): Observable<any> {
    return this.http.get(this.baseUrl + `api/visitproduct/${email}`);
  }

  registerUser(customer: Customer) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/register', JSON.stringify(customer), { headers });
  }

  getAllCustomers() {
    return this.http.get(this.baseUrl + 'weatherforecast');
  }

  uploadProduct(product: Product): Observable<any> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/product', JSON.stringify(product), { headers });
  }

  uploadCart(cart: Cart): Observable<any> {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/cart', JSON.stringify(cart), { headers });
  }

  uploadComment(comment: ProductComment) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/comment', JSON.stringify(comment), { headers });
  }

  getAllComment(product: Product): Observable<any>{
    return this.http.get(this.baseUrl + `api/comment/${product.id}`);
  }

  setLike(like: Like){
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/like', JSON.stringify(like), { headers });
  }

  getLikes(customer: Customer): Observable<any>{
    return this.http.get(this.baseUrl + `api/like/${customer.email}`);
  }

  getCartByUser(customer: Customer): Observable<any>{
    return this.http.get(this.baseUrl + `api/cart/${customer.email}`);
  }

  uploadBalance(customer: Customer): Observable<any>{
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/customer', JSON.stringify(customer), { headers });
  }

  getCategories(): Observable<any> {
    return this.http.get(this.baseUrl + 'api/category');
  }

  addToVisitedProduct(customer: Customer, product: Product){
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(this.baseUrl + 'api/visitproduct', JSON.stringify({customer, product}), { headers }).subscribe();
  }

  deleteProduct(product: Product): Observable<any>{
    return this.http.delete(this.baseUrl + `api/product/${product.id}`);
  }

  deleteCustomer(customer: Customer): Observable<any>{
    return this.http.delete(this.baseUrl + `api/customer/${customer.email}`);
  }

  updateProduct(product: Product): Observable<any>{
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(this.baseUrl + `api/product/${product.id}`, JSON.stringify(product), { headers });
  }

  updateUser(customer: Customer) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(this.baseUrl + 'api/customer/1', JSON.stringify(customer), { headers });
  }
}
