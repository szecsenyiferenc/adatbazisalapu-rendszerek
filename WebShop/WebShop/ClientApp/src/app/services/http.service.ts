import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginData } from '../models/login-data.model';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

private baseUrl = document.getElementsByTagName('base')[0].href;


constructor(private http: HttpClient) { }

checkLogin(loginData: LoginData): Observable<any>{
  let headers = new HttpHeaders({'Content-Type': 'application/json'});
  return this.http.post(this.baseUrl + 'api/login', JSON.stringify(loginData), {headers});
}

getProducts(): Observable<any>{
  return this.http.get(this.baseUrl + 'api/product');
}

registerUser(customer: Customer){
  let headers = new HttpHeaders({'Content-Type': 'application/json'});
  return this.http.post(this.baseUrl + 'api/register', JSON.stringify(customer), {headers}).subscribe(a => console.log(a));
}

getAllCustomers(){
  return this.http.get(this.baseUrl + 'weatherforecast');
}

}
