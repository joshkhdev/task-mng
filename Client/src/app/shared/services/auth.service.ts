import { Injectable, Signal, signal } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, ReplaySubject, switchMap, take, tap } from 'rxjs';
import { LoginUserRequest, RegisterUserRequest, UserResponse } from '../../api/generated/models';
import { UsersApiService } from '../../api/generated/services';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authorized$ = new ReplaySubject<boolean>(1);
  private readonly user = signal<UserResponse | null>(null);

  constructor(
    private readonly usersApi: UsersApiService,
    private readonly router: Router,
  ) {
    this.getCurrentUser().subscribe();
  }

  public get isAuthorized(): Observable<boolean> {
    return this.authorized$.asObservable();
  }

  public get currentUser(): Signal<UserResponse | null> {
    return this.user.asReadonly();
  }

  public register(registerRequest: RegisterUserRequest): Observable<void> {
    return this.usersApi.registerUser({ body: registerRequest });
  }

  public login(loginRequest: LoginUserRequest): Observable<UserResponse> {
    return this.usersApi.authSignIn({ body: loginRequest }).pipe(
      switchMap(() => this.getCurrentUser()),
    );
  }

  public logout(): Observable<void> {
    return this.usersApi.authSignOut().pipe(
      tap(() => this.onSessionClose()),
      take(1),
    );
  }

  public getCurrentUser(): Observable<UserResponse> {
    return this.usersApi.getCurrentUser().pipe(
      tap({
        next: (userResponse: UserResponse) => {
          this.user.set(userResponse);
          this.authorized$.next(true);
        },
        error: () => {
          this.onSessionClose();
        },
      }),
      take(1),
    );
  }

  private onSessionClose(): void {
    this.user.set(null);
    this.authorized$.next(false);
    this.router.navigateByUrl('/auth');
  }
}
