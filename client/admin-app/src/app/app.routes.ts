import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { applicationAuthGuard, loginGuard } from './core/guards/auth.guard';
import { BasicLayoutComponent } from './components/layout/basic-layout.component';
import { OrganizationsComponent } from './pages/organizations/organizations.component';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
    { path: '', redirectTo: 'app', pathMatch: 'full' },
    {
        path: 'app', component: BasicLayoutComponent,
        data: {
            breadcrumb: 'App'
        },
        canActivate: [applicationAuthGuard], canActivateChild: [applicationAuthGuard],
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
                component: OrganizationsComponent,
                title: 'Organizations',
                data: {
                    breadcrumb: 'Organizations'
                },
                canActivate: [adminGuard]
            },
        ]
    },
    { path: 'login', component: LoginComponent, title: 'Login into Ticky', pathMatch: 'full', canActivate: [loginGuard] },
    { path: '**', component: NotFoundComponent }
];
