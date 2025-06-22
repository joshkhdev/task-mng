import { Component, DestroyRef, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTimepickerModule } from '@angular/material/timepicker';
import { TaskEntityStatus, TaskResponse } from '../../../api/generated/models';
import { ActivatedRoute } from '@angular/router';
import { TasksApiService } from '../../../api/generated/services';
import { MatSelectModule } from '@angular/material/select';
import { catchError, finalize, map, Observable, of, switchMap, take, throwError } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { formatDateTime, formatTimeSpent } from '../../../shared/utils/date.utils';
import { TaskStatusNames } from '../../../shared/interfaces/task.interfaces';

@Component({
  selector: 'app-task-info',
  templateUrl: './task-info.component.html',
  styleUrl: './task-info.component.scss',
  imports: [
    MatButtonModule,
    MatDatepickerModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    MatTimepickerModule,
    FormsModule,
  ],
  providers: [provideNativeDateAdapter()],
})
export class TaskInfoComponent implements OnInit {
  public readonly isLoading = signal(false);
  public readonly isError = signal<boolean>(false);

  public readonly TaskStatuses = TaskEntityStatus;
  public readonly TaskStatusNames = TaskStatusNames;

  public readonly taskInfo = signal<TaskResponse | undefined>(undefined);
  public readonly status = signal<TaskEntityStatus | undefined>(undefined);

  public readonly formatDateTime = formatDateTime;
  public readonly formatTimeSpent = formatTimeSpent;

  constructor(
    private readonly tasksApi: TasksApiService,
    private readonly route: ActivatedRoute,
    private readonly destroy: DestroyRef,
  ) {
  }

  public ngOnInit(): void {
    this.isLoading.set(true);
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = +(params.get('id') ?? 0);

        if (!id) {
          this.isError.set(true);
          return of(void 0);
        }

        return this.loadTaskInfo(id);
      }),
      catchError(err => {
        this.isError.set(true);

        return throwError(() => err);
      }),
      finalize(() => this.isLoading.set(false)),
      take(1),
      takeUntilDestroyed(this.destroy),
    ).subscribe();
  }

  public loadTaskInfo(id: number): Observable<void> {
    return this.tasksApi.getTaskInfo({ id }).pipe(
      map(task => {
        this.taskInfo.set(task);
        this.status.set(task.status);

        return (void 0);
      })
    );
  }

  public changeStatus(value: TaskEntityStatus): void {
    const task = this.taskInfo();
    if (!task || task.status === value) {
      return;
    }

    this.isLoading.set(true);
      this.tasksApi.updateTask({ id: task.id, body: { status: value }}).pipe(
        switchMap(() => this.loadTaskInfo(task.id)),
        finalize(() => this.isLoading.set(false)),
        take(1),
      ).subscribe();
  }
}
