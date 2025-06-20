import { Component, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { UsersApiService } from '../../../api/generated/services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  imports: [
    MatButtonModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
  ],
})
export class LoginComponent {
  public readonly showPassword = signal(false);

  public readonly loginFormGroup: FormGroup<{
    login: FormControl<string>,
    password: FormControl<string>,
  }>;

  constructor(
    private readonly usersApi: UsersApiService,
    private readonly router: Router,
    private readonly fb: FormBuilder,
  ) {
    this.loginFormGroup = this.fb.nonNullable.group({
      login: this.fb.nonNullable.control('', Validators.required),
      password: this.fb.nonNullable.control('', Validators.required),
    });
  }


  public togglePasswordVisibility(): void {
    this.showPassword.update(v => !v);
  }

  public login(): void {
    if (this.loginFormGroup.invalid) {
      return;
    }

    this.usersApi.authSignIn({ body: this.loginFormGroup.getRawValue() }).subscribe(() => {
      this.router.navigateByUrl('/');
    });
  }
}
