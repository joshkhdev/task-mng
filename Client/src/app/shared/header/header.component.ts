import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  imports: [
    MatButtonModule,
    RouterLink,
  ],
})
export class HeaderComponent {
  constructor(private readonly router: Router) {}

  public get hideLogin(): boolean {
    return this.router.url.includes('login');
  }

  public get hideRegister(): boolean {
    return this.router.url.includes('register');
  }
}
