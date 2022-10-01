import { partitionArray } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommunicationService } from '../communication.service';
import { LDRService } from '../ldr.service';
import { ConnectionParameters } from '../models/ConnectionParameters';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  isConnected: boolean = false
  constructor(private communicationService: CommunicationService, private ldrService: LDRService, private router: Router) {
    
  }

  ngOnInit(): void {
    this.communicationService.isConnected().subscribe((data) => {
      this.isConnected = data
      console.log(" ngoninit status: " + this.isConnected)
    })
  }

  onConnectButtonClick() {
    var parameters = new ConnectionParameters(1, 1, 1, 1, 1)
    this.communicationService.connect(parameters).subscribe()
    this.isConnected = true
    console.log("status: " + this.isConnected)
  }
  onDisconnectButtonClick() {
    var parameters = new ConnectionParameters(1, 1, 1, 1, 1)
    this.communicationService.disconnect(parameters).subscribe()
    this.isConnected = false;
    console.log("status: " + this.isConnected)
  }
  onManualButtonClick() {

    //navigate to manual component
    this.router.navigate(['/manual-mode'])
  }
  onAutomaticButtonClick() {
    //navigate to automatic component
    this.router.navigate(['/automatic-mode'])
  }
}
