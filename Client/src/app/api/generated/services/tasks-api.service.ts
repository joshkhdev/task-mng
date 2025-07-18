/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ApiBaseService } from '../api-base-service';
import { ApiConfiguration } from '../api-configuration';
import { ApiStrictHttpResponse } from '../api-strict-http-response';

import { createComment } from '../fn/tasks/create-comment';
import { CreateComment$Params } from '../fn/tasks/create-comment';
import { createTask } from '../fn/tasks/create-task';
import { CreateTask$Params } from '../fn/tasks/create-task';
import { deleteComment } from '../fn/tasks/delete-comment';
import { DeleteComment$Params } from '../fn/tasks/delete-comment';
import { deleteTask } from '../fn/tasks/delete-task';
import { DeleteTask$Params } from '../fn/tasks/delete-task';
import { getAllTasks } from '../fn/tasks/get-all-tasks';
import { GetAllTasks$Params } from '../fn/tasks/get-all-tasks';
import { getTaskInfo } from '../fn/tasks/get-task-info';
import { GetTaskInfo$Params } from '../fn/tasks/get-task-info';
import { TaskResponse } from '../models/task-response';
import { TaskShortResponse } from '../models/task-short-response';
import { updateTask } from '../fn/tasks/update-task';
import { UpdateTask$Params } from '../fn/tasks/update-task';

@Injectable({ providedIn: 'root' })
export class TasksApiService extends ApiBaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `getAllTasks()` */
  static readonly GetAllTasksPath = '/api/tasks';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllTasks()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllTasks$Response(params?: GetAllTasks$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<Array<TaskShortResponse>>> {
    return getAllTasks(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getAllTasks$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllTasks(params?: GetAllTasks$Params, context?: HttpContext): Observable<Array<TaskShortResponse>> {
    return this.getAllTasks$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<Array<TaskShortResponse>>): Array<TaskShortResponse> => r.body)
    );
  }

  /** Path part for operation `createTask()` */
  static readonly CreateTaskPath = '/api/tasks';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `createTask()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  createTask$Response(params: CreateTask$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<TaskResponse>> {
    return createTask(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `createTask$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  createTask(params: CreateTask$Params, context?: HttpContext): Observable<TaskResponse> {
    return this.createTask$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<TaskResponse>): TaskResponse => r.body)
    );
  }

  /** Path part for operation `getTaskInfo()` */
  static readonly GetTaskInfoPath = '/api/tasks/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getTaskInfo()` instead.
   *
   * This method doesn't expect any request body.
   */
  getTaskInfo$Response(params: GetTaskInfo$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<TaskResponse>> {
    return getTaskInfo(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getTaskInfo$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getTaskInfo(params: GetTaskInfo$Params, context?: HttpContext): Observable<TaskResponse> {
    return this.getTaskInfo$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<TaskResponse>): TaskResponse => r.body)
    );
  }

  /** Path part for operation `deleteTask()` */
  static readonly DeleteTaskPath = '/api/tasks/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `deleteTask()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteTask$Response(params: DeleteTask$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<void>> {
    return deleteTask(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `deleteTask$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteTask(params: DeleteTask$Params, context?: HttpContext): Observable<void> {
    return this.deleteTask$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `updateTask()` */
  static readonly UpdateTaskPath = '/api/tasks/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `updateTask()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  updateTask$Response(params: UpdateTask$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<void>> {
    return updateTask(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `updateTask$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  updateTask(params: UpdateTask$Params, context?: HttpContext): Observable<void> {
    return this.updateTask$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `createComment()` */
  static readonly CreateCommentPath = '/api/tasks/{taskId}/comments';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `createComment()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  createComment$Response(params: CreateComment$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<void>> {
    return createComment(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `createComment$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  createComment(params: CreateComment$Params, context?: HttpContext): Observable<void> {
    return this.createComment$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `deleteComment()` */
  static readonly DeleteCommentPath = '/api/tasks/{taskId}/comments/{commentId}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `deleteComment()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteComment$Response(params: DeleteComment$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<void>> {
    return deleteComment(this.http, this.rootUrl, params, context);
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `deleteComment$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteComment(params: DeleteComment$Params, context?: HttpContext): Observable<void> {
    return this.deleteComment$Response(params, context).pipe(
      map((r: ApiStrictHttpResponse<void>): void => r.body)
    );
  }

}
