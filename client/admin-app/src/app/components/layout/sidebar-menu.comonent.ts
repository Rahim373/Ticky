import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { IsGrantedDirective } from '../../core/directives/is-granted.directive';

@Component({
  selector: 'app-sidebar-menu',
  standalone: true,
  imports: [NzMenuModule, RouterLink, IsGrantedDirective],
  template: `
    <ul nz-menu nzMode="inline" class="sider-menu">
      <li nz-menu-item nz-tooltip nzTooltipPlacement="right" nzMatchRouter >
        <a [routerLink]="['dashboard']">Dashboard</a>
      </li>
      <li nz-menu-item nz-tooltip nzTooltipPlacement="right" nzMatchRouter *isGranted="'Admin'">
      <a [routerLink]="['organizations']">Organizations</a>
      </li>      
    </ul>
  `
})
export class SidebarMenuComponent {
}
