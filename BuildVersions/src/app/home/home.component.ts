import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { ResponseObject } from '../model/response-object';
import { BuildVersion } from "../model/build-version";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
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
