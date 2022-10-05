import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ManualModeService } from '../manual-mode.service';
import { ManualModeData } from '../models/ManualModeData';

function currentValidator(control: FormControl): { [s: string]: boolean } | null {
  if (Number(control.value) < 4 || Number(control.value) > 20) {
    return { invalidValue: true }
  }
  else {
    return null
  }
}

@Component({
  selector: 'app-manual-mode',
  templateUrl: './manual-mode.component.html',
  styleUrls: ['./manual-mode.component.css']
})
export class ManualModeComponent implements OnInit {
  error: string
  ManualModeForm: FormGroup
  state: boolean
  currValue: string = ""
  relayState: string = ""
  isSubmit: boolean
  submitted = false
  constructor(fb: FormBuilder, private manualModeService: ManualModeService) {
    this.ManualModeForm = fb.group({
      'currentValue': ['', Validators.compose([Validators.required, currentValidator])],
      'relayState': ['', Validators.required]
    })
    this.isSubmit = false
    this.state = false
    this.error = "Current Value is required"
  }

  ngOnInit(): void {

  }

  onManualSubmit(value: any) {
    this.submitted = true;

    if (this.ManualModeForm.invalid) {
      console.log("Invalid")
    }
    else {
      this.isSubmit = true
      console.log(value.relayState)
      console.log(value.currentValue)
      this.currValue = value.currentValue
      this.relayState = value.relayState
      

      if (value.relayState == "ON")
        this.state = true;
      else
        this.state = false;

      var manualModeData = new ManualModeData(value.currentValue, this.state)

      this.manualModeService.sendCurrentAndRelayData(manualModeData).subscribe()
      this.submitted = false
      this.ManualModeForm.reset()
      //Object.keys(this.ManualModeForm.controls).forEach((key) => {
      //  const control = this.ManualModeForm.controls[key];
      //  control.markAsPristine();
      //  control.markAsUntouched();
      //});
      //Object.keys(this.ManualModeForm.controls).forEach((key) => {
      //  const control = this.ManualModeForm.controls[key];
      //  control.setErrors(null);
      //});
    }
    
  }
  get f() { return this.ManualModeForm.controls }

}




//import { Component, OnInit } from '@angular/core';
//import { FormBuilder, FormGroup, Validators } from '@angular/forms';
//import { ManualModeService } from '../manual-mode.service';
//import { ManualModeData } from '../models/ManualModeData';

//@Component({
//  selector: 'app-manual-mode',
//  templateUrl: './manual-mode.component.html',
//  styleUrls: ['./manual-mode.component.css']
//})
//export class ManualModeComponent implements OnInit {
//  ManualModeForm : FormGroup
//  state : boolean
//  constructor(fb:FormBuilder, private manualModeService:ManualModeService) {
//    this.ManualModeForm = fb.group({
//      'currentValue':['',Validators.required],
//      'relayState':['',Validators.required]
//    })

//    this.state = false
//   }

//  ngOnInit(): void {

//  }

//  onManualSubmit(value:any)
//  {
//    console.log(value.relayState)
//    console.log(value.currentValue)
//    this.ManualModeForm.reset()
    
//    if(value.relayState == "ON")
//      this.state = true;
//    else
//      this.state = false;

//    var manualModeData = new ManualModeData(value.currentValue,this.state)
    
//    this.manualModeService.sendCurrentAndRelayData(manualModeData).subscribe()
    
//  }

//}
