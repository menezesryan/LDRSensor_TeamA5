import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Chart, registerables } from 'chart.js';
import { CommunicationService } from '../communication.service';
import { ConnectionParameters } from '../models/ConnectionParameters';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  portArray: string[] = []
  connectionForm: FormGroup
  constructor(private communicationService: CommunicationService, fb: FormBuilder, private router: Router) {
    this.connectionForm = fb.group({
      'port': [0],
      'baud': [9600],
      'dataBit': [1],
      'startBit': [1],
      'stopBit': [1],
      'parityBit': [1],
    })
  }

  ngOnInit(): void {
    this.communicationService.getPortNames().subscribe((data) => {
      this.portArray = data
    })
  }

  onChangeType(evt: any) {
    var portName = evt.target.value.split(':')[1].trim()
    console.log(portName)

  }

  onConnectButtonClick() {
    var parameters = new ConnectionParameters(1, 1, 1, 1, 1)
    this.communicationService.connect(parameters).subscribe(() => {
      location.reload()
    })

    this.router.navigate(['/automatic-mode'])
  }

}
