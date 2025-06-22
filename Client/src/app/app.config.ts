import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { routes } from './app.routes';
import { ApiConfiguration } from './api/generated/api-configuration';
import { environment } from '../environments/environment';
import { MAT_DATE_LOCALE } from '@angular/material/core';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    {
      provide: ApiConfiguration,
      useValue: {
        rootUrl: environment.apiUrl,
      },
    },
    {
      provide: MAT_DATE_LOCALE,
      useValue: 'ru-RU',
    },
  ],
};
