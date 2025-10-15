import { Routes } from '@angular/router';
import { Countries } from './countries/countries';
import { Providers } from './Providers/providers';
import { Services } from './Services/services';
import { Screen404 } from './Screen404/screen404';

export const routes: Routes = [
  { path: '', redirectTo: '/countries', pathMatch: 'full' },
  { path: 'countries', component: Countries },  
  { path: 'providers', component: Providers },
  { path: 'services', component: Services },
  { path: '**', component: Screen404 }
];
