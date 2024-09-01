import { Component, Input } from '@angular/core';
import { ReactiveFormsModule, NonNullableFormBuilder, Validators } from '@angular/forms';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzFormControlComponent, NzFormItemComponent, NzFormModule } from 'ng-zorro-antd/form';
import { NzInputDirective } from 'ng-zorro-antd/input';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { OrganizationsService } from '../../../services/organizations.service';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-invitation-modal',
  standalone: true,
  imports: [NzModalModule, NzButtonComponent, NzFormModule, NzFormControlComponent,
    NzFormItemComponent, ReactiveFormsModule, NzInputDirective, NzIconModule],
  templateUrl: './invitation-modal.component.html'
})
export class InvitationModalComponent {
  @Input() inviteAsOrgOwner: boolean = false;
  @Input() btnName: string = '';
  @Input() modalTitle: string = '';

  isVisible: boolean = false;
  inviteLoading: boolean = false;

  invitationForm = this.fb.group({
    email: ['', [Validators.email, Validators.required]]
  });

  constructor(private fb: NonNullableFormBuilder,
    private orgService: OrganizationsService,
    private messageService: NzMessageService
  ) { }

  open() {
    this.isVisible = true;
  }

  async handleInvite() {
    if (!this.invitationForm.valid)
      return;

    try {
      this.inviteLoading = true;
      await this.orgService.inviteUserAsync(this.invitationForm.value.email!, this.inviteAsOrgOwner);
      this.messageService.success("Successfully invited.");
      this.handleClose();
    } finally {
      this.inviteLoading = false;
    }
  }

  handleClose(): void {
    this.isVisible = false;
    this.invitationForm.reset();
  }
}
