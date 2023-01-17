import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'

import { environment } from './../../environments/environment';
import { BuildVersion } from '../model/build-version';
import { ResponseObject } from '../model/response-object';

@Component({
  selector: 'app-build-versions',
  templateUrl: './build-versions.component.html',
  styleUrls: ['./build-versions.component.scss']
})
export class BuildVersionsComponent implements OnInit {
  title = 'BuildVersions';
  public buildVersions?: BuildVersion[];

  constructor(http: HttpClient) {
    console.log(environment.baseUrl + '/buildversions/getall');
    http.get<ResponseObject<BuildVersion[]>>(environment.baseUrl + '/buildversions/getall').
      subscribe(result => {
        if (result)
          if (result.succeeded)
            this.buildVersions = result.data;
          else
            console.error(result.message)
      }, error => console.error(error));
  }

  ngOnInit(): void {
  }
}
