import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { parseDate } from 'ngx-bootstrap/chronos';

@Component({
  selector: 'app-user-dairies',
  templateUrl: './user-dairies.component.html',
  styleUrls: ['./user-dairies.component.css']
})
export class UserDairiesComponent implements OnInit {
 users;
 dates;
 d;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.getUserDairies();
  }



  getUserDairies(){
    this.userService.getUser().subscribe(res=>{
      this.users = res;
     

    //  this.dates  =   this.users.dairies.map((e, i )=>{
    //                  let d=  parseDate(e.date)
    //         console.log(d.getFullYear() );
    //   });

      console.log(typeof this.dates);
    }, error=>{
      console.log(error);
    });
  }

}
