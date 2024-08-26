import { Directive, inject, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { RoleEnum } from '../models/role';
import { AuthService } from '../../services/auth.service';

@Directive({
  selector: '[isGranted]',
  standalone: true
})
export class IsGrantedDirective {
  private _rolePermission!: RoleEnum;
  private _authService = inject(AuthService);
  private _templateRef = inject(TemplateRef);
  private _viewContainer = inject(ViewContainerRef);

  @Input()
  set isGranted(rolePermission: string) {
    this._rolePermission = rolePermission as RoleEnum;
  }

  ngOnInit() {
    if (this._authService.isGranted(this._rolePermission)) {
      this._viewContainer.clear();
      this._viewContainer.createEmbeddedView(this._templateRef);
    }
  }
}
