import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LDRService } from '../ldr.service';
import { LightThreshold } from '../models/LightThreshold';

@Component({
  selector: 'app-set-threshold',
  templateUrl: './set-threshold.component.html',
  styleUrls: ['./set-threshold.component.css']
})
export class SetThresholdComponent implements OnInit {
  ThresholdForm : FormGroup
  lightThreshold : LightThreshold
  constructor(fb:FormBuilder, private ldrService : LDRService) {
    this.ThresholdForm = fb.group({
      'lowerThreshold':['',Validators.required],
      'upperThreshold' :['',Validators.required]
    })

    this.lightThreshold = new LightThreshold(0,0)
   }

  ngOnInit(): void {
  }

  onThresholdSumbit(value:any)
  {
    console.log(value.lowerThreshold)
    console.log(value.upperThreshold)
    this.lightThreshold = new LightThreshold(value.lowerThreshold, value.upperThreshold)

    this.ldrService.setThresholdValues(this.lightThreshold).subscribe()

    this.ThresholdForm.reset()
  }

  onResetThresholdSubmit()
  {
    //get the default threshold values
    //make a new threshold object and store these values
    this.lightThreshold = new LightThreshold(0,0)
    this.ldrService.resetThresholdValues().subscribe()
  }

}
