import {
  ApplicationConfig,
  importProvidersFrom,
  provideBrowserGlobalErrorListeners,
  provideZonelessChangeDetection
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, provideHttpClient, withFetch} from '@angular/common/http';
import { TokenInterceptorService } from '../../services/token-interceptor.services';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes), provideClientHydration(withEventReplay()),

    // add formsModule to interact with ngModel in the forms
    importProvidersFrom(FormsModule),

    // add http client for api calls
    provideHttpClient(withFetch()),

    // add jwt interceptor to attach the tokens automatically
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
  ]
};
