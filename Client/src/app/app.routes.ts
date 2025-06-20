import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthComponent } from './pages/auth/auth.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { TasksComponent } from './pages/tasks/tasks.component';
import { TaskListComponent } from './pages/tasks/task-list/task-list.component';
import { TaskInfoComponent } from './pages/tasks/task-info/task-info.component';
import { MainComponent } from './pages/main/main.component';

export const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    pathMatch: 'full',
  },
  {
    path: 'auth',
    component: AuthComponent,
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
    children: [
      {
        path: '',
        component: TaskListComponent,
        pathMatch: 'full',
      },
      {
        path: ':id',
        component: TaskInfoComponent,
      },
    ],
  },
];
