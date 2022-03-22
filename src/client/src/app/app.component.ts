import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Home', url: '/dashboard', icon: 'apps'},
    { title: 'Objects', url: '/objects', icon: 'cube' },
    { title: 'History', url: '/history', icon: 'git-branch' },
    { title: 'Settings', url: '/settings', icon: 'settings' }
  ];

  constructor() {}
}
