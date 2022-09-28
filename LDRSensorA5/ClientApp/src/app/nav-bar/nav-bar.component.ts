import { Component, OnInit } from '@angular/core';
import { CommunicationService } from '../communication.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private communicationService: CommunicationService) { }

  ngOnInit(): void {
  }

  onConnectButtonClick()
  {
    this.communicationService.connect().subscribe()
  }
  onDisconnectButtonClick()
  {
    this.communicationService.disconnect().subscribe()
  }
  onManualButtonClick()
  {
    //navigate to manual component
  }
  onAutomaticButtonClick()
  {
    //navigate to automatic component
  }
}
