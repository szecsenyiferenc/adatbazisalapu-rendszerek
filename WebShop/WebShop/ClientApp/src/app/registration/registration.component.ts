import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http.service';
import { Customer } from '../models/customer.model';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  lname: string;
  fname: string;
  tel: string;
  cit: string;
  str: string;
  hnum: number;

  constructor(private httpService: HttpService, private router: Router, private loginService: LoginService) { }

  ngOnInit() {
    if(this.loginService.customer$ && this.loginService.customer$.value){
      this.lname = this.loginService.customer$.value.lastName;
      this.fname = this.loginService.customer$.value.firstName;
      this.tel = this.loginService.customer$.value.phone;
      this.cit = this.loginService.customer$.value.city;
      this.str = this.loginService.customer$.value.street;
      this.hnum = this.loginService.customer$.value.houseNumber;
    }
  }

  submit(f){
    let newCustomer: Customer = f.value;

    let currentCustomer = this.loginService.customer$.value;

    if(currentCustomer){
      newCustomer.email = currentCustomer.email;
      newCustomer.balance = currentCustomer.balance;
      newCustomer.isRegularCustomer = currentCustomer.isRegularCustomer;
      newCustomer.houseNumber = +newCustomer.houseNumber;
      newCustomer.isAdmin = currentCustomer.isAdmin;

      this.httpService.updateUser(newCustomer).subscribe((success: Customer) =>{
        if(success){
          this.loginService.customer$.next(success)
          this.router.navigate(['/profile']);
        }
      });
    }
    else{
      newCustomer.balance = 0;
      newCustomer.isRegularCustomer = false;
      newCustomer.houseNumber = +newCustomer.houseNumber;
      newCustomer.isAdmin = false;

      this.httpService.registerUser(newCustomer).subscribe(success =>{
        if(success){
          this.router.navigate(['/login']);
        }
      });
    }


  }

}
