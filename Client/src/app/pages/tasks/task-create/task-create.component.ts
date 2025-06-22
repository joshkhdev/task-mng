import { Component, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatTimepickerModule } from '@angular/material/timepicker';
import { Router } from '@angular/router';
import { TasksApiService } from '../../../api/generated/services';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrl: './task-create.component.scss',
  imports: [
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatTimepickerModule,
    ReactiveFormsModule,
  ],
  providers: [provideNativeDateAdapter()],
})
export class TaskCreateComponent {
  public readonly isLoading = signal(false);

  public readonly taskCreateFormGroup: FormGroup<{
    name: FormControl<string>,
    description: FormControl<string>,
    plannedCompletionDate: FormControl<Date>,
  }>;

  constructor(
    private readonly tasksApi: TasksApiService,
    private readonly router: Router,
    private readonly fb: FormBuilder,
  ) {
    this.taskCreateFormGroup = this.fb.nonNullable.group({
      name: this.fb.nonNullable.control('', Validators.required),
      description: this.fb.nonNullable.control('', Validators.required),
      plannedCompletionDate: this.fb.nonNullable.control(new Date(), Validators.required),
    });
  }

  public create(): void {
    if (this.taskCreateFormGroup.invalid) {
      return;
    }

    const { name, description, plannedCompletionDate } = this.taskCreateFormGroup.getRawValue();
    const plannedCompletionDateIso = new Date(plannedCompletionDate).toISOString();

    this.isLoading.set(true);
    this.tasksApi.createTask({ body: {
      name,
      description,
      plannedCompletionDate: plannedCompletionDateIso,
    }}).pipe(
      finalize(() => this.isLoading.set(false)),
    ).subscribe(task => {
      this.router.navigateByUrl(`/tasks/${task.id}`);
    });
  }
}
