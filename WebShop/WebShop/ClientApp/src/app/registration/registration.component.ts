import { Component, OnInit } from '@angular/core';
import { HttpService } from '../services/http.service';
import { Customer } from '../models/customer.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private httpService: HttpService) { }

  ngOnInit() {
  }

  submit(f){
    let newCustomer: Customer = f.value;
    newCustomer.balance = 0;
    newCustomer.isRegularCustomer = false;
    newCustomer.houseNumber = +newCustomer.houseNumber;
    console.log("NC", newCustomer);
    this.httpService.registerUser(newCustomer);
  }

}
