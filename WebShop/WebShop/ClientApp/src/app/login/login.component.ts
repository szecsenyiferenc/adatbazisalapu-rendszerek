import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService) {
    this.loginService.customer$.subscribe(a => console.log("CUSTOMER", a));
   }

  ngOnInit() {
  }

  submit(f){
    this.loginService.login(f.value)
  }

}
