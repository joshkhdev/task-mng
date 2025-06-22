import { Component, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  imports: [
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
  ],
})
export class LoginComponent {
  public readonly showPassword = signal(false);
  public readonly isLoading = signal(false);

  public readonly loginFormGroup: FormGroup<{
    login: FormControl<string>,
    password: FormControl<string>,
  }>;

  constructor(
    private readonly authService: AuthService,
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

    this.isLoading.set(true);
    this.authService.login(this.loginFormGroup.getRawValue()).pipe(
      finalize(() => this.isLoading.set(false)),
    ).subscribe(() => {
      this.router.navigateByUrl('/');
    });
  }
}
