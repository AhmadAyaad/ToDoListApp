import { Component, OnInit } from '@angular/core';
import {AuthService} from 'src/app/_services/auth.service';
import {  Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
 
  constructor(private authService : AuthService , private router:Router ) { }
 userModel: any = {};
  ngOnInit() {
  }


  login(){
    this.authService.login(this.userModel).subscribe((res)=>{
      console.log("logged in success");
      this.router.navigate(['/dairy']);
    }, err=>{
      console.log(err);
    });
  }
  isLogedIn(){ 
  return  this.authService.isLoggedIn();
  }
  logout(){
    localStorage.removeItem('token');
  }

}
