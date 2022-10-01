import { Component, OnDestroy } from '@angular/core';
import { Chart } from 'chart.js';
import { CommunicationService } from './communication.service';
import { ConnectionParameters } from './models/ConnectionParameters';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnDestroy {
  title = 'app';

  constructor(private communicationService:CommunicationService)
  {
  }

  ngOnDestroy(): void {
    var parameters = new ConnectionParameters(1,1,1,1,1);
     this.communicationService.disconnect(parameters).subscribe();
     console.log("closed connection")
  }  
}
