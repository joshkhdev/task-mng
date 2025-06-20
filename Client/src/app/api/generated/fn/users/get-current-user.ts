/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { ApiStrictHttpResponse } from '../../api-strict-http-response';
import { ApiRequestBuilder } from '../../api-request-builder';

import { UserResponse } from '../../models/user-response';

export interface GetCurrentUser$Params {
}

export function getCurrentUser(http: HttpClient, rootUrl: string, params?: GetCurrentUser$Params, context?: HttpContext): Observable<ApiStrictHttpResponse<UserResponse>> {
  const rb = new ApiRequestBuilder(rootUrl, getCurrentUser.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'application/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as ApiStrictHttpResponse<UserResponse>;
    })
  );
}

getCurrentUser.PATH = '/api/users/current';
