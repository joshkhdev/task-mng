import { Routes } from '@angular/router';
import { AuthComponent } from './pages/auth/auth.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { TaskListComponent } from './pages/tasks/task-list/task-list.component';
import { authComponentGuard, authGuard } from './shared/guards/auth.guard';
import { TaskEditComponent } from './pages/tasks/task-edit/task-edit.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/tasks',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    component: AuthComponent,
    canActivate: [authComponentGuard],
    children: [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full',
      },
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'register',
        component: RegisterComponent,
      },
    ],
  },
  {
    path: 'tasks',
    component: TasksComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        component: TaskListComponent,
        pathMatch: 'full',

      },
      {
        path: 'create',
        component: TaskEditComponent,
      },
      {
        path: ':id',
        component: TaskEditComponent,
      },
    ],
  },
  {
    path: '**',
    redirectTo: '/',
  },
];
