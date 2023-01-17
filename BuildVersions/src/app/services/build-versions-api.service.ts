import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { ResponseObject } from './../model/response-object'
import { BuildVersion } from './../model/build-version'

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
  providedIn: 'root'
})
export class BuildVersionsApiService {
  private http: HttpClient;
  private baseUrl: string;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
  }

  createProject(buildVersion: BuildVersion) {
    var url = this.baseUrl + '/buildversions/createproject';
    //this.http.post<ResponseObject<BuildVersion>>(url, buildVersion)
    this.http.post<BuildVersion>(url, buildVersion)
      .subscribe(result => {
        //console.log("BuildVersion " + result.data.id + " has been created.");
        console.log("BuildVersion " + result.id + " has been created.");
        return true;
      }, error => {
        console.error(error);
      });
    return false;
  }

  updateProject(buildVersion: BuildVersion) {
    var url = this.baseUrl + '/buildversions/updateproject';
    //this.http.put<ResponseObject<BuildVersion>>(url, buildVersion)
    this.http.put<BuildVersion>(url, buildVersion)
      .subscribe(result => {
        //console.log("BuildVersion " + result.data.id + " has been updated.");
        console.log("BuildVersion " + result.id + " has been updated.");
        return true;
      }, error => {
        console.error(error);
      });
    return false;
  }

  getVersionById(id: number): BuildVersion|null {
    var url = this.baseUrl + "/buildversions/getversionbyid?id=" + id;
    //this.http.get<ResponseObject<BuildVersion>>(url)
    this.http.get<BuildVersion>(url)
      .subscribe(result => {
        console.log(result);
        //return result.data;
        return result;
      }, error => {
        console.error(error);
        return null;
      });
    return null;
  }

  getVersionByName(projectName: string): BuildVersion | null {
    var url = this.baseUrl + "/buildversions/getversionbyname?name=" + projectName;
    //this.http.get<ResponseObject<BuildVersion>>(url)
    this.http.get<BuildVersion>(url)
      .subscribe(result => {
        console.log(result);
        //return result.data;
        return result;
      }, error => {
        console.error(error);
      });
    return null;
  }

  getAll(): BuildVersion[] | null {
    var url = this.baseUrl + "/buildversions/getall";
    //this.http.get<ResponseObject<BuildVersion[]>>(url)
    this.http.get<BuildVersion[]>(url)
      .subscribe(result => {
        console.log(result);
        //return result.data;
        return result;
      }, error => {
        console.error(error);
      });
    return null;
  }

}
