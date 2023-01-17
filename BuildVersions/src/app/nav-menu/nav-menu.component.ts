import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})

export class NavMenuComponent implements OnInit, OnDestroy {

  constructor(private router: Router) {
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
  }
}
