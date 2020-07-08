import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {FormsModule} from '@angular/forms';
import { FileUploadModule } from 'ng2-file-upload';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';


import { AppComponent } from './app.component';
import {NavComponent} from 'src/app/nav/nav.component';
import { routes } from 'src/app/routes';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DairyComponent } from './dairy/dairy.component';
import {AuthService} from '../app/_services/auth.service';
import {UserService} from '../app/_services/user.service';
import { UserDairiesComponent } from './user-dairies/user-dairies.component';
import {AuthGuard} from './_guards/auth.guard';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      DairyComponent,
      UserDairiesComponent,
   ],
   imports: [
      BrowserModule,
      FormsModule,
      BsDatepickerModule.forRoot(),
      FileUploadModule,
      HttpClientModule,
      RouterModule.forRoot(routes), NoopAnimationsModule
   ],
   providers: [AuthService , UserService , AuthGuard],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { } 