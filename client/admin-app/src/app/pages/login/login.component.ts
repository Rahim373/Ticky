import { NgOptimizedImage } from '@angular/common';
import { Component } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxComponent } from 'ng-zorro-antd/checkbox';
import { NzFormControlComponent, NzFormModule } from 'ng-zorro-antd/form';
import { NzInputDirective, NzInputGroupComponent } from 'ng-zorro-antd/input';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [NgOptimizedImage, NzFormModule, NzInputDirective, NzCheckboxComponent,
    NzFormControlComponent, NzInputGroupComponent, NzButtonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginLoading: boolean = false;  

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  loginForm = this.formBuilder.group({
    email: ['rahim.prsf@gmail.com', [Validators.required, Validators.email]],
    password: ['Temppass1!', [Validators.required]],
    remember: [true]
  });

  async submitForm() {
    if (this.loginForm.valid) {
      this.loginLoading = true;
      await this.authService.login({
        email: this.loginForm.value.email!,
        password: this.loginForm.value.password!
      });

      this.loginLoading = false;

    } else {
      Object.values(this.loginForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
}
