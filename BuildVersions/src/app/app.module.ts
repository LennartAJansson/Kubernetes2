import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AngularMaterialModule } from './angular-material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HealthCheckComponent } from './health-check/health-check.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BuildVersionsComponent } from './build-versions/build-versions.component';
import { BuildVersionEditComponent } from './build-versions/build-version-edit.component';
import { environment } from '../environments/environment';
import {
  API_BASE_URL,
  BuildVersionsApiService,
} from './services/build-versions-api.service';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HealthCheckComponent,
    BuildVersionsComponent,
    BuildVersionEditComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AngularMaterialModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.apiUrl },
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: 'outline' },
    },
    BuildVersionsApiService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
