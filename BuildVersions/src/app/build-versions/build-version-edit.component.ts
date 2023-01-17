import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'

import { environment } from './../../environments/environment';
import { BuildVersion } from '../model/build-version';
import { ResponseObject } from '../model/response-object';
import { BuildVersionsApiService } from '../services/build-versions-api.service';

@Component({
  selector: 'app-build-version-edit',
  templateUrl: './build-version-edit.component.html',
  styleUrls: ['./build-version-edit.component.scss']
})
export class BuildVersionEditComponent implements OnInit {

  title?: string;
  form!: FormGroup

  buildVersion?: BuildVersion;
  // the BuildVersion object id, as fetched from the active route:
  // It's NULL when we're adding a new BuildVersion,
  // and not NULL when we're editing an existing one.
  id?: number;

  buildVersions?: BuildVersion[];

  constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: BuildVersionsApiService,
    private http: HttpClient) {

  }

  ngOnInit(): void {
    this.form = this.fb.group({
      projectName: ['',
        Validators.required,
      ],
      major: ['',
        [
          Validators.required,
        ],
      ],
      minor: ['',
        [
          Validators.required,
        ],
      ],
      build: ['',
        [
          Validators.required,
        ],
      ],
      revision: ['',
        [
          Validators.required,
        ],
      ],
      semanticVersionText: ['',
        [
          Validators.required,
        ],
      ]
    });

    this.loadData();
  }

  loadData() {
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;
    if (this.id) {
      // EDIT MODE
        var url = environment.baseUrl + "/buildversions/getversionbyid/" + this.id;
      this.http.get<ResponseObject<BuildVersion>>(url)
        .subscribe(result => {
          this.buildVersion = result.data;
          this.title = "Edit - " + this.buildVersion.projectName;
          this.form.patchValue(this.buildVersion);
        }, error => console.error(error));
    }
    else {
      // ADD NEW MODE
      this.title = "Create a new BuildVersion";
    }
  }

  onSubmit() {
    var buildversion = (this.id) ? this.buildVersion : <BuildVersion>{};
    if (buildversion) {
      buildversion.projectName = this.form.controls['projectName'].value;
      buildversion.major = this.form.controls['major'].value;
      buildversion.minor = this.form.controls['minor'].value;
      buildversion.build = this.form.controls['build'].value;
      buildversion.revision = this.form.controls['revision'].value;
      buildversion.semanticVersionText = this.form.controls['semanticVersionText'].value;
      if (this.id) {
        // EDIT mode
        buildversion.id = this.id!;
        var url = environment.baseUrl + '/buildversions/updateproject';
        this.http.put<ResponseObject<BuildVersion>>(url, buildversion)
          .subscribe(result => {
            console.log("BuildVersion " + buildversion!.id + " has been updated.");
            this.router.navigate(['/buildversions']);
          }, error => console.error(error));
      }
      else {
        // ADD NEW mode
        buildversion.id = 0;
        var url = environment.baseUrl + '/buildversions/createproject';
        this.http.post<ResponseObject<BuildVersion>>(url, buildversion)
          .subscribe(result => {
            console.log("BuildVersion " + result.data.id + " has been created.");
            this.router.navigate(['/buildversions']);
          }, error => console.error(error));
      }
    }
  }
}
