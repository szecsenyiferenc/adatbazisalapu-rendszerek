import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { map, tap } from 'rxjs/operators';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { LoginService } from './login.service';
import { Cart } from '../models/cart.model';
import { ProductComment } from '../models/comment.model';
import { Like } from '../models/like.model';
import { LikedProduct } from '../models/likedProduct.model';
import { Customer } from '../models/customer.model';
import { HttpHeaders } from '@angular/common/http';
import { Category } from '../models/category.model';
import { VisitedProduct } from '../models/visitedProduct.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  cart: CartItem[];
  products: Product[];
  categories: Category[];
  categories$: BehaviorSubject<Category[]>;
  selectedCategory$: BehaviorSubject<Category>;
  likes: any[];
  selectedProduct: Product;

  constructor(private httpService: HttpService, private loginService: LoginService) {
    this.cart = [];
    this.categories$ = new BehaviorSubject<Category[]>([]);
    this.selectedCategory$ = new BehaviorSubject<Category>(null);
  }


  getProducts(): Observable<Product[]> {
    this.selectedProduct = null;
    return this.httpService.getProducts().pipe(tap(products => this.products = products));
  }

  getProductById(id: string): Observable<LikedProduct> {
    let selectedProduct = this.products.find(p => p.id === +id);
    return of(selectedProduct);
  }

  getVisitedProducts(): Observable<VisitedProduct[]> {
    return this.httpService.getVisitedProducts(this.loginService.customer$.value.email);
  }

  addItemToCart(product: Product, quantity: number) {
    let productInCart = this.cart.find(c => c.product.id === product.id);
    if (productInCart) {
      productInCart.quantity += quantity;
    }
    else {
      this.cart.push({ product, quantity });
    }
  }

  deleteItemFromCart(cartItem: CartItem) {
    const index = this.cart.indexOf(cartItem);
    if (index > -1) {
      this.cart.splice(index, 1);
    }
  }

  uploadProduct(product: Product): Observable<any> {
    return this.httpService.uploadProduct(product);
  }

  uploadCart(cartItems: CartItem[]): Observable<any> {
    const cart: Cart = {
      customer: this.loginService.customer$.value,
      cartItems
    }
    return this.httpService.uploadCart(cart);
  }

  uploadComment(comment: ProductComment){
    return this.httpService.uploadComment(comment);
  }

  getAllComment(product: Product): Observable<ProductComment[]>{
    if(product){
      return this.httpService.getAllComment(product);
    }
    return of(null);
  }

  setLike(like: Like){
    return this.httpService.setLike(like);
  }

  getLikes(){
    if(this.loginService.customer$.value){
      return this.httpService.getLikes(this.loginService.customer$.value);
    }
    return of(null);
  }

  getLikesFromProduct(product: Product){
    if(this.loginService.customer$.value){
      return this.httpService.getLikes(this.loginService.customer$.value)
      .pipe(map(likes => {
        const like = likes.find(element => element.id === product.id);
        return like && like.value;
      }));
    }
    return of(null);
  }

  getCartByUser(){
    return this.httpService.getCartByUser(this.loginService.customer$.value);
  }

  uploadBalance(plusBalance: number){
    let customer: Customer = this.loginService.customer$.value;
    customer.balance += plusBalance;
    return this.httpService.uploadBalance(customer);
  }

  getCategories(): Observable<Category[]> {
    return this.httpService.getCategories().pipe(tap(categories => this.categories$.next(categories)));
  }

  selectCategory(category: Category){
    this.selectedCategory$.next(category);
  }

  addToVisitedProduct(product: Product){
    if(this.loginService.customer$.value){
      this.httpService.addToVisitedProduct(this.loginService.customer$.value , product);
    }
  }

  deleteProduct(product: Product): Observable<any>{
    return this.httpService.deleteProduct(product);
  }

  updateProduct(product: Product): Observable<any> {
    return this.httpService.updateProduct(product);
  }

  getStockByProduct(product: Product){
    return this.httpService.getStockByProduct(product);
  }
}