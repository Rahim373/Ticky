import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ApplicationAuthGuard, LoginGuard } from './core/guards/auth.guard';
import { BasicLayoutComponent } from './components/layout/basic-layout.component';
import { OrganizationsComponent } from './pages/organizations/organizations.component';

export const routes: Routes = [
    { path: '', redirectTo: 'app', pathMatch: 'full' },
    {
        path: 'app', component: BasicLayoutComponent,
        data: {
            breadcrumb: 'App'
        },
        canActivate: [ApplicationAuthGuard], canActivateChild: [ApplicationAuthGuard],
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            {
                path: 'dashboard',
                component: DashboardComponent,
                title: 'Dashboard',
                data: {
                    breadcrumb: 'Dashboard'
                }
            },
            { 
                path: 'organizations', 
                component: OrganizationsComponent ,
                title: 'Organizations',
                data: {
                    breadcrumb: 'Organizations'
                  }
            },
        ]
    },
    { path: 'login', component: LoginComponent, title: 'Login into Ticky', pathMatch: 'full', canActivate: [LoginGuard] },
    { path: '**', component: NotFoundComponent }
];
