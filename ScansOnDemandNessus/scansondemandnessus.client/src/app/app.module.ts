import { HttpClientModule } from '@angular/common/http';
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

@NgModule({
  declarations: [
    NavMenuComponent,
    ScansComponent,
    ResultsComponent,
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule,
    FontAwesomeModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: ScansComponent, pathMatch: 'full' },
      { path: 'results', component: ResultsComponent },
    ]),
  ],
  providers: [
    { provide: 'BASE_URL', useValue: environment.baseUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
