import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ManualModeService } from '../manual-mode.service';

@Component({
  selector: 'app-manual-mode',
  templateUrl: './manual-mode.component.html',
  styleUrls: ['./manual-mode.component.css']
})
export class ManualModeComponent implements OnInit {
  ManualModeForm : FormGroup
  state : boolean
  constructor(fb:FormBuilder, private manualModeService:ManualModeService) {
    this.ManualModeForm = fb.group({
      'currentValue':['',Validators.required],
      'relayState':['',Validators.required]
    })

    this.state = false
   }

  ngOnInit(): void {

  }

  onManualSubmit(value:any)
  {
    console.log(value.relayState)
    console.log(value.currentValue)
    this.ManualModeForm.reset()
    
    if(value.relayState == "ON")
      this.state = true;
    else
      this.state = false;

    this.manualModeService.setCurrentValue(value.currentValue).subscribe()
    this.manualModeService.setRelayState(this.state).subscribe()
    
  }

}
