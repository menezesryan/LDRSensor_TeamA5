import { APP_BOOTSTRAP_LISTENER, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Chart, registerables } from 'chart.js';
import { CommunicationService } from '../communication.service';
import { LDRService } from '../ldr.service';
import { ConnectionParameters } from '../models/ConnectionParameters';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  portArray: string[] = []
  portName: string
  connectionForm: FormGroup
  constructor(private communicationService: CommunicationService, fb: FormBuilder, private router: Router, private ldrService: LDRService) {
    this.connectionForm = fb.group({
      'port': [],
      'baud': [9600],
      'dataBit': [8],
      'startBit': [1],
      'stopBit': [1],
      'parityBit': ["None"],
    })
    this.portName = ""
  }

  ngOnInit(): void {
    this.communicationService.getPortNames().subscribe((data) => {
      this.portArray = data
    })
  }

  onChangeType(evt: any) {
    this.portName = evt.target.value.split(':')[1].trim()
    console.log(this.portName)

  }

  onConnectButtonClick(value: any) {
    var parameters = new ConnectionParameters(this.portName, value.baud, value.dataBit, value.startBit, value.stopBit, 0)

    this.communicationService.connect(parameters).subscribe(() => {
      console.log("in")

      this.ldrService.getThresholdData().subscribe({
        next:(data)=>{
          // if(data)
          // {
            console.log("innmnnnn")
            this.router.navigate(['/automatic-mode'])
            .then(() => {
              window.location.reload()
            })
          // }
          // else
          // {
          //   alert("error")
          //   this.communicationService.disconnect(parameters).subscribe()
          // }
          },
        error: ()=> {
          alert("error")
          this.communicationService.disconnect(parameters).subscribe()
        }
      })

      // this.ldrService.getThresholdData().subscribe((data) => {
      //   console.log("Second in")
      //   this.router.navigate(['/automatic-mode'])
      //     .then(() => {
      //       window.location.reload()
      //     })
      // }, (error) => {
      //   alert("error")
      //   this.communicationService.disconnect(parameters);
      // })
    })
  }

}
