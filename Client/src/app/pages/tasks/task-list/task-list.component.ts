import { Component, DestroyRef, OnInit, signal } from '@angular/core';
import { TasksApiService } from '../../../api/generated/services';
import { TaskShortResponse } from '../../../api/generated/models';
import { finalize } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { TaskStatusNames } from '../../../shared/interfaces/task.interfaces';
import { formatDateTime } from '../../../shared/utils/date.utils';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss',
  imports: [
    MatButtonModule,
    MatDividerModule,
    MatProgressSpinnerModule,
    RouterLink,
  ],
})
export class TaskListComponent implements OnInit {
  public readonly isLoading = signal<boolean>(false);

  public readonly tasks = signal<TaskShortResponse[]>([]);

  public readonly TaskStatusNames = TaskStatusNames;
  public readonly formatDateTime = formatDateTime;

  constructor(
    private readonly tasksApi: TasksApiService,
    private readonly destroy: DestroyRef,
  ) {}

  public ngOnInit(): void {
    this.loadTasks();
  }

  public loadTasks(): void {
    this.isLoading.set(true);
    this.tasksApi.getAllTasks().pipe(
      finalize(() => this.isLoading.set(false)),
      takeUntilDestroyed(this.destroy),
    ).subscribe(tasks => this.tasks.set(tasks));
  }
}
