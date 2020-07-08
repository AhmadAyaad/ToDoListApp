import { DairyComponent } from './dairy/dairy.component';
import { Routes } from '@angular/router';
import { UserDairiesComponent } from './user-dairies/user-dairies.component';
import { AuthGuard } from './_guards/auth.guard';

export const routes: Routes = [
    { path: 'dairy', component:DairyComponent , canActivate: [AuthGuard] },
    {path:'mydairies' , component: UserDairiesComponent , canActivate:[AuthGuard]} ,
  ];