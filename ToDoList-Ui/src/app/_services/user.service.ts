import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {usersUrl} from '../_config/api';
import{AuthService} from '../_services/auth.service';
import { from } from 'rxjs';


const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + localStorage.getItem('token'),
  }),
};
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private authService : AuthService) { }

  getUser(){

    return this.http.get(usersUrl + '/' + this.authService.decodedToken.nameid , httpOptions);
  }

}
