import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommunicationService } from '../communication.service';
import { ConnectionParameters } from '../models/ConnectionParameters';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private communicationService: CommunicationService, private router:Router) { }

  ngOnInit(): void {
  }

  onConnectButtonClick()
  {
    var parameters = new ConnectionParameters(1,1,1,1,1)
    this.communicationService.connect(parameters).subscribe()
  }
  onDisconnectButtonClick()
  {
    var parameters = new ConnectionParameters(1,1,1,1,1)
    this.communicationService.disconnect(parameters).subscribe()
  }
  onManualButtonClick()
  {
    
    //navigate to manual component
    this.router.navigate(['/manual-mode'])
  }
  onAutomaticButtonClick()
  {
    //navigate to automatic component
    this.router.navigate(['/automatic-mode'])
  }
}
