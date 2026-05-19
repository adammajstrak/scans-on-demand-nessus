
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import {provideZoneChangeDetection} from '@angular/core';

platformBrowserDynamic().bootstrapModule(AppModule, { applicationProviders: [provideZoneChangeDetection({ eventCoalescing: true })], })
  .catch(err => console.error(err));
