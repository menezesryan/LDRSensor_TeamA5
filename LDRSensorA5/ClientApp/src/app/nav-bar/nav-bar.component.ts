import { partitionArray } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  myForm: FormGroup
  isConnected: boolean
  constructor(private communicationService: CommunicationService, private ldrService: LDRService, private router: Router, fb: FormBuilder) {
    this.isConnected = this.communicationService.connectStatus
    this.myForm = fb.group({
      'port': ['COM3'],
      'baud': [9600],
      'dataBit': [1],
      'startBit': [1],
      'stopBit': [1],
      'parityBit': [1],
    })
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
    this.isConnected = this.communicationService.connectStatus
    console.log("status: " + this.isConnected)
    document.getElementById('connectModalButton')?.click()
    this.router.navigate(['/automatic-mode'])
  }
  onDisconnectButtonClick() {
      var parameters = new ConnectionParameters(1, 1, 1, 1, 1)
      this.communicationService.disconnect(parameters).subscribe()
      this.isConnected = this.communicationService.connectStatus
      console.log("status: " + this.isConnected)
    this.router.navigate(['/home'])
    document.getElementById('disconnectModalButton')?.click()
    //var parameters = new ConnectionParameters(1, 1, 1, 1, 1)
    //this.communicationService.disconnect(parameters).subscribe()
    //this.isConnected = false;
    //console.log("status: " + this.isConnected)
    //this.router.navigate(['/home'])
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
