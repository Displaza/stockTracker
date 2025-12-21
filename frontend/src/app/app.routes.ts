// https://angular.dev/guide/routing/define-routes

import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { AppComponent } from './app.component';
import { FormComponent } from './features/form/form.component';
import { PageNoFoundComponent } from './pagenotfound.component';
import { LoginComponent } from './features/auth/login.component';
import { RegisterComponent } from './features/auth/register.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { authGuard } from './features/auth/auth.guard';
import { SymbolsComponent } from './features/content/symbols.component';

export const routes: Routes = [
    {path: '', redirectTo: 'app', pathMatch: 'full'},
    {path: 'app', component: AppComponent},
    {path: 'home', component: HomeComponent},
    {path: 'form', component: FormComponent},
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
    { path: 'symbols', component: SymbolsComponent },
    {path:'**', component: PageNoFoundComponent}, 
];
