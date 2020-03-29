import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http.service';
import { Customer } from '../models/customer.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private httpService: HttpService, private router: Router) { }

  ngOnInit() {
  }

  submit(f){
    let newCustomer: Customer = f.value;
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
