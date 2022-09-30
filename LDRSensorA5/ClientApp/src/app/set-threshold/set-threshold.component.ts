import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { LDRService } from '../ldr.service';
import { LightThreshold } from '../models/LightThreshold';

@Component({
  selector: 'app-set-threshold',
  templateUrl: './set-threshold.component.html',
  styleUrls: ['./set-threshold.component.css']
})
export class SetThresholdComponent implements OnInit, OnDestroy{
  ThresholdForm : FormGroup
  lightThreshold : LightThreshold
  saveLightThreshold:LightThreshold
  setThresholdSubscription : Subscription
  resetThresholdSubscription : Subscription

  constructor(fb:FormBuilder, private ldrService : LDRService) {
    this.ThresholdForm = fb.group({
      'lowerThreshold':['',Validators.required],
      'upperThreshold' :['',Validators.required]
    })

    this.saveLightThreshold = new LightThreshold(0,0)
    this.lightThreshold = new LightThreshold(0,0)
    this.setThresholdSubscription = Subscription.EMPTY
    this.resetThresholdSubscription = Subscription.EMPTY
   }
  ngOnDestroy(): void {
    if(this.setThresholdSubscription)
      this.setThresholdSubscription.unsubscribe()
    
    if(this.resetThresholdSubscription)
      this.resetThresholdSubscription.unsubscribe()
  }

  ngOnInit(): void {
    this.ldrService.getThresholdData().subscribe(data=>{
      this.lightThreshold = data
    })
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
    this.ldrService.resetThresholdValues(this.lightThreshold).subscribe(()=>{
      this.ldrService.getDefaultThresholdData().subscribe(data=>{
        this.lightThreshold = data
        console.log(this.lightThreshold)
      })
    }
    )
    

  }

  onSaveThresholdSubmit()
  {
    this.ldrService.saveThresholdData(this.lightThreshold).subscribe()
  }
}
