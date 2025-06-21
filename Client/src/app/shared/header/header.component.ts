import { Component, Signal, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserResponse } from '../../api/generated/models';
import { finalize } from 'rxjs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  imports: [
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterLink,
  ],
})
export class HeaderComponent {
  public readonly authorized: Signal<boolean | undefined>;
  public readonly isLoading = signal<boolean>(false);

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router,
  ) {
    this.authorized = toSignal(this.authService.isAuthorized);
  }

  public get currentUser(): UserResponse | null {
    return this.authService.currentUser();
  }

  public get hideLogin(): boolean {
    return this.router.url.includes('login');
  }

  public get hideRegister(): boolean {
    return this.router.url.includes('register');
  }

  public logout(): void {
    this.isLoading.set(true);
    this.authService.logout().pipe(
      finalize(() => this.isLoading.set(false)),
    ).subscribe();
  }
}
