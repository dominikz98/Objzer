import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public appPages = [
    { title: 'Home', url: '/dashboard', icon: 'apps' },
    { title: 'Catalogue', url: '/catalogue', icon: 'book' },
    { title: 'History', url: '/history', icon: 'git-branch' },
    { title: 'Settings', url: '/settings', icon: 'settings' }
  ];

  public urlsegments: UrlSegment[];

  constructor(private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => this.urlsegments = this.createBreadcrumbs(this.activatedRoute.root));
  }

  private createBreadcrumbs(route: ActivatedRoute, url: string = '', breadcrumbs: UrlSegment[] = []): UrlSegment[] {
    const children: ActivatedRoute[] = route.children;

    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      const urlsegment: string = child.snapshot.url.map(segment => segment.path).join('/');

      if (urlsegment !== '') {
        url += `/${urlsegment}`;
        breadcrumbs.push({ href: url, name: this.capitalizeFirstLetter(urlsegment) });
      }

      return this.createBreadcrumbs(child, url, breadcrumbs);
    }
  }

  capitalizeFirstLetter(text: string) {
    return text.charAt(0).toUpperCase() + text.slice(1);
  }
}

export class UrlSegment {
  href: string;
  name: string;
}