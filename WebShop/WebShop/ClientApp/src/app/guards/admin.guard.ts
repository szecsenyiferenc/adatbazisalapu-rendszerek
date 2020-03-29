import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { LoginService } from "../services/login.service";
import { map } from 'rxjs/operators';

@Injectable()
export class AdminGuard implements CanActivate {
  constructor(private loginService: LoginService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.loginService.customer$.pipe(map(
        customer => customer && customer.isAdmin 
    ));
  }
}