import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { LDRService } from '../ldr.service';
import { LightThreshold } from '../models/LightThreshold';


function lowerValidator(control: FormControl): { [s: string]: boolean } | null {
  if (!(Number(control.value) < 200 && Number(control.value) > 10)) {
    return { invalidLowerValue: true }
  }
  else {
    return null
  }
}
function upperValidator(control: FormControl): { [s: string]: boolean } | null {
  if (!(Number(control.value) < 250 && Number(control.value) > 50)) {
    return { invalidUpperValue: true }
  }
  else {
    return null
  }
}
function LowerUpperCondition(lowerThreshold: string, upperThreshold: string) {
  return (formGroup: FormGroup) => {
    const lower = formGroup.controls[lowerThreshold]
    const upper = formGroup.controls[upperThreshold]

    if (upper.errors && !upper.errors['lowerUpperCondition']) {
      return
    }

    if (Number(lower.value) > Number(upper.value)) {
      upper.setErrors({ lowerUpperCondition: true })

    }
    else {
      upper.setErrors(null)
    }

  }

}


@Component({
  selector: 'app-set-threshold',
  templateUrl: './set-threshold.component.html',
  styleUrls: ['./set-threshold.component.css']
})
export class SetThresholdComponent implements OnInit, OnDestroy {
  ThresholdForm: FormGroup
  lightThreshold: LightThreshold
  saveLightThreshold: LightThreshold
  setThresholdSubscription: Subscription
  resetThresholdSubscription: Subscription
  submitted = false

  constructor(fb: FormBuilder, private ldrService: LDRService) {
    this.ThresholdForm = fb.group({
      'lowerThreshold': ['', Validators.compose([Validators.required, lowerValidator])],
      'upperThreshold': ['', Validators.compose([Validators.required, upperValidator])]
    },

      { validator: LowerUpperCondition('lowerThreshold', 'upperThreshold') })

    this.saveLightThreshold = new LightThreshold(0, 0)
    this.lightThreshold = new LightThreshold(0, 0)
    this.setThresholdSubscription = Subscription.EMPTY
    this.resetThresholdSubscription = Subscription.EMPTY

    // this.ldrService.getThresholdData().subscribe(data => {
    //   this.lightThreshold = data
    // })
  }
  ngOnDestroy(): void {
    if (this.setThresholdSubscription)
      this.setThresholdSubscription.unsubscribe()

    if (this.resetThresholdSubscription)
      this.resetThresholdSubscription.unsubscribe()
  }

  ngOnInit(): void {
    this.ldrService.getThresholdData().subscribe(data => {
      this.lightThreshold = data
      
      console.log("hi" + this.lightThreshold.lowerThreshold + " " + this.lightThreshold.upperThreshold)
    })
  }

  onThresholdSumbit(value: any) {
    this.submitted = true;

    if (this.ThresholdForm.invalid) {
      console.log("Invalid")
    }
    else {
      console.log(value.lowerThreshold)
      console.log(value.upperThreshold)
      this.lightThreshold = new LightThreshold(value.lowerThreshold, value.upperThreshold)

      this.ldrService.setThresholdValues(this.lightThreshold).subscribe()
      this.submitted = false;
      this.ThresholdForm.reset()
    }
  }

  onResetThresholdSubmit() {
    //get the default threshold values
    //make a new threshold object and store these values
    this.lightThreshold = new LightThreshold(0, 0)
    this.ldrService.resetThresholdValues(this.lightThreshold).subscribe(() => {
      this.ldrService.getDefaultThresholdData().subscribe(data => {
        this.lightThreshold = data
        console.log(this.lightThreshold)
      })
    }
    )


  }

  onSaveThresholdSubmit() {
    this.ldrService.saveThresholdData(this.lightThreshold).subscribe()
    console.log(this.lightThreshold.lowerThreshold)
    console.log(this.lightThreshold.upperThreshold)
  }
  get f() { return this.ThresholdForm.controls }
}
