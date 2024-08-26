import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NzContentComponent, NzFooterComponent, NzHeaderComponent, NzLayoutComponent, NzSiderComponent } from 'ng-zorro-antd/layout';
import { SidebarMenuComponent } from "./sidebar-menu.comonent";
import { NzBreadCrumbComponent, NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzDividerComponent } from 'ng-zorro-antd/divider';
import { AuthService } from '../../services/auth.service';
import { ContainerHeaderComponent } from "./container-header";

@Component({
  selector: 'app-basic-layout',
  standalone: true,
  imports: [RouterOutlet, NzLayoutComponent, NzHeaderComponent, NzSiderComponent, NzDividerComponent,
    NzContentComponent, NzFooterComponent, SidebarMenuComponent, NzBreadCrumbComponent, ContainerHeaderComponent],
  template: `
  <nz-layout>
      <nz-header nzTheme="light">
        <a class="text-white float-right" nz-button nzType="text" (click)="logout()">Logout</a>
      </nz-header>
      <nz-layout>
        <nz-sider nzWidth="200px" nzTheme="light">
          <app-sidebar-menu></app-sidebar-menu>
        </nz-sider>
        <nz-content class="p-10 h-[calc(100vh-4rem)]">
          <app-container-header></app-container-header>
          <router-outlet></router-outlet>
        </nz-content>
      </nz-layout>
      <!-- <nz-footer>Footer</nz-footer> -->
    </nz-layout>
  `
})
export class BasicLayoutComponent {
  constructor (private authService: AuthService){}

  logout() {
    this.authService.logout();
  }
}
