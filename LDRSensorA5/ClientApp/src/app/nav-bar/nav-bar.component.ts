import { Component, OnInit } from '@angular/core';
import { ConnectionService } from '../connection.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private connectionService: ConnectionService) { }

  ngOnInit(): void {
  }

  onConnectButtonClick()
  {
    this.connectionService.connect().subscribe()
  }
  onDisconnectButtonClick()
  {
    this.connectionService.disconnect().subscribe()
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
