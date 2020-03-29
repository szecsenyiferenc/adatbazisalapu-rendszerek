import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router, private loginService: LoginService) {
    this.loginService.customer$.subscribe(a => console.log("CUSTOMER", a));
   }

  ngOnInit() {
  }

  submit(f){
    this.loginService.login(f.value).subscribe(customer => {
      if(customer){
        this.router.navigate(['/products']);
      }
    });
  }

}
