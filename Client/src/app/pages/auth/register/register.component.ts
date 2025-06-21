import { Component, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { Router } from '@angular/router';
import { finalize, switchMap } from 'rxjs';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  imports: [
    MatButtonModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
  ],
})
export class RegisterComponent {
  public readonly showPassword = signal(false);
  public readonly isLoading = signal(false);

  public readonly registerFormGroup: FormGroup<{
    name: FormControl<string>,
    login: FormControl<string>,
    password: FormControl<string>,
  }>;

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router,
    private readonly fb: FormBuilder,
  ) {
    this.registerFormGroup = this.fb.nonNullable.group({
      name: this.fb.nonNullable.control('', Validators.required),
      login: this.fb.nonNullable.control('', Validators.required),
      password: this.fb.nonNullable.control('', Validators.required),
    });
  }

  public togglePasswordVisibility(): void {
    this.showPassword.update(v => !v);
  }

  public register(): void {
      if (this.registerFormGroup.invalid) {
        return;
      }

      const userData = this.registerFormGroup.getRawValue();

      this.isLoading.set(true);
      this.authService.register(userData).pipe(
        switchMap(() => this.authService.login(userData)),
        finalize(() => this.isLoading.set(false)),
      ).subscribe(() => {
        this.router.navigateByUrl('/');
      });
    }
}
