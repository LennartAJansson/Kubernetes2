//import { Injectable } from '@angular/core';
//import { HttpClient } from '@angular/common/http';
//import { Observable, Subject, tap } from 'rxjs';

//import { environment } from './../../environments/environment';
//import { LoginRequest } from './../model/login-request';
//import { LoginResponse } from '../model/login-response';
//import { RegisterRequest } from './../model/register-request';
//import { RegisterResponse } from '../model/register-response';
//import { ResponseObject } from '../model/response-object';
//import { UsersInfoRequest } from '../model/users-info-request';
//import { UserInfo } from '../model/user-info';

//@Injectable({
//  providedIn: 'root',
//})

//export class AuthService {

//  private tokenKey: string = "token";
//  private usernameKey: string = "username";
//  private roleKey: string = "role";

//  private _authStatus = new Subject<boolean>();
//  public authStatus = this._authStatus.asObservable();

//  constructor(
//    protected http: HttpClient) {
//  }

//  isAuthenticated(): boolean {
//    return this.getToken() !== null;
//  }

//  getToken(): string | null {
//    return localStorage.getItem(this.tokenKey);
//  }

//  getUserName(): string | null {
//    return localStorage.getItem(this.usernameKey);
//  }

//  init(): void {
//    if (this.isAuthenticated())
//      this.setAuthStatus(true);
//  }

//  register(item: RegisterRequest): Observable<ResponseObject<RegisterResponse>> {
//    var url = environment.baseUrl + "/auth/register";
//    return this.http.post<ResponseObject<RegisterResponse>>(url, item)
//      .pipe(tap(registerResult => {
//        if (registerResult.status >= 200 && registerResult.status < 300 && registerResult.data) {
//        }
//      }));
//  }

//  login(item: LoginRequest): Observable<ResponseObject<LoginResponse>> {
//    var url = environment.baseUrl + "/auth/signin";
//    return this.http.post<ResponseObject<LoginResponse>>(url, item)
//      .pipe(tap(loginResult => {
//        if (loginResult.status >= 200 && loginResult.status < 300 && loginResult.data) {
//          localStorage.setItem(this.tokenKey, loginResult.data.token);
//          localStorage.setItem(this.usernameKey, loginResult.data.username);
//          localStorage.setItem(this.roleKey, loginResult.data.role);
//          this.setAuthStatus(true);
//        }
//      }));
//  }

//  logout() {
//    localStorage.removeItem(this.tokenKey);
//    localStorage.removeItem(this.usernameKey);
//    localStorage.removeItem(this.roleKey);
//    this.setAuthStatus(false);
//  }

//  getUserInfo(user: string): Observable<ResponseObject<UserInfo>> {
//    var url = environment.baseUrl + "/auth/getuser/" + user;
//    return this.http.get<ResponseObject<UserInfo>>(url)
//      .pipe(tap(result => {
//        if (result.status && result.data) {
//        }
//      }));
//  }

//  updateUserInfo(user: UserInfo): Observable<ResponseObject<UserInfo>> {
//    var url = environment.baseUrl + "/auth/updateuser";
//    return this.http.post<ResponseObject<UserInfo>>(url, user)
//      .pipe(tap(result => {
//        if (result.status && result.data) {
//        }
//      }));
//  }

//  getUsersInfo(item: UsersInfoRequest): Observable<ResponseObject<UserInfo[]>> {
//    var url = environment.baseUrl + "/auth/getusers";
//    return this.http.get<ResponseObject<UserInfo[]>>(url, item)
//      .pipe(tap(usersInfoResult => {
//        if (usersInfoResult.status && usersInfoResult.data) {
//        }
//      }));
//  }

//  private setAuthStatus(isAuthenticated: boolean): void {
//    this._authStatus.next(isAuthenticated);
//  }
//}
