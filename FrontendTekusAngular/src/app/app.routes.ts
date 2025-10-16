import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Countries } from './countries/countries';
import { Providers } from './Providers/providers';
import { Services } from './Services/services';
import { Screen404 } from './Screen404/screen404';
import { AuthGuard } from './auth/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'countries', component: Countries, canActivate: [AuthGuard] },  
  { path: 'providers', component: Providers, canActivate: [AuthGuard] },
  { path: 'services', component: Services, canActivate: [AuthGuard] },
  { path: '**', component: Screen404 }
];
