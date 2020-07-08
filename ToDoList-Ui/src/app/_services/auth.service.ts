import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';  
import { map } from 'rxjs/operators';
import {loginUrl} from 'src/app/_config/api';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser;



  constructor(private http: HttpClient) { }

  login(userData){
    return this.http.post(loginUrl , userData).pipe(

    map((response:any)=>{
      const user = response;
      if(user){
        localStorage.setItem('token' , user.token);
        localStorage.setItem('user', user.userToReturnDto);
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        console.log(this.decodedToken);
        this.currentUser = user.userToReturnDto;

      }
    })

    );
  }

    isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
