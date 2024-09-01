import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Data, NavigationEnd, Router, TitleStrategy } from '@angular/router';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { filter, map, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-container-header',
  standalone: true,
  imports: [NzPageHeaderModule, NzBreadCrumbModule, NzSpaceModule],
  template: `
    <nz-page-header>
      <nz-breadcrumb nz-page-header-breadcrumb [nzAutoGenerate]="true"></nz-breadcrumb>
      <nz-page-header-title>{{title}}</nz-page-header-title>
    </nz-page-header>
  `
})
export class ContainerHeaderComponent {
  @Input() title!: string;

}
