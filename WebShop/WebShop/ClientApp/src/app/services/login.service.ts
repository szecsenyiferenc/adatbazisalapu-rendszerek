import { Injectable, OnDestroy } from '@angular/core';
import { Customer } from '../models/customer.model';
import { HttpService } from './http.service';
import { LoginData } from '../models/login-data.model';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService implements OnDestroy{
  customer$: BehaviorSubject<Customer>;
  loginSubscription: Subscription;

constructor(private httpService: HttpService) { 
  this.customer$ = new BehaviorSubject<Customer>(null);
}


login(loginData: LoginData){
  this.httpService.checkLogin(loginData)
  .subscribe(customer => this.customer$.next(customer));
}

logout(){
  this.customer$.next(null);
}

ngOnDestroy(): void {
  this.loginSubscription.unsubscribe();
}

}
