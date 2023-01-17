import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BuildVersionsComponent } from './build-versions/build-versions.component';
import { BuildVersionEditComponent } from './build-versions/build-version-edit.component';
import { HealthCheckComponent } from './health-check/health-check.component';

const routes: Routes = [
  { path: 'buildversions', component: BuildVersionsComponent, pathMatch: 'full' },
  { path: 'buildversionedit/:id', component: BuildVersionEditComponent },
  { path: 'buildversionedit', component: BuildVersionEditComponent },
  { path: 'health-check', component: HealthCheckComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
