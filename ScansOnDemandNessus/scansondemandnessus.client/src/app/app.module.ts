import { provideHttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component'; // Importuj 
import { ResultsComponent } from './results/results.component';
import { ScansComponent } from './scans/scans.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { RouterModule } from '@angular/router';
import { environment } from '../environments/environment';
import { SettingsComponent } from './settings/settings.component';

@NgModule({
  declarations: [
    NavMenuComponent,
    ScansComponent,
    ResultsComponent,
    SettingsComponent,
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule,
    FontAwesomeModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: ScansComponent, pathMatch: 'full' },
      { path: 'results', component: ResultsComponent },
      { path: 'settings', component: SettingsComponent },
    ]),
  ],
  providers: [
    { provide: 'BASE_URL', useValue: environment.baseUrl },
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
