import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ApplicationAuthGuard, LoginGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {path: '', redirectTo: 'login', pathMatch: 'full'},
    {path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate: [ApplicationAuthGuard]},
    {path: 'login', component: LoginComponent, pathMatch: 'full', canActivate: [LoginGuard]},
    {path: '**', component: NotFoundComponent, pathMatch: 'full'}
];
