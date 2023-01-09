import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'shipment-integration-web';

  constructor(public routerService: Router) {
    this.routerService.navigate(['orders']);
  }
}
